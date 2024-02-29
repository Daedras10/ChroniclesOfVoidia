using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace BattleEntity
{
    public class UnitVisual : MovingBattleEntity
    {
        [Header("Components")]
        [SerializeField] private MeleeRangeDetector meleeRangeDetector;
        
        [Header("Settings")]
        [SerializeField] protected float attackDistance = 1.2f;
        [field:SerializeField] public Unit Unit { get; private set; }

        
        private List<UnitVisual> unitsInRange = new ();
        
        
        private event Action TryAttack;
        public Action<UnitVisual> OnUnitSelected;
        public Action<UnitVisual> OnUnitDeselected;
        public Action<UnitVisual> OnUnitTakeDamage;
        public static event Action<UnitVisual,UnitVisual> OnUnitEnterMeleeRange;
        public static event Action<UnitVisual,UnitVisual> OnUnitExitMeleeRange;
        
        protected override void Init()
        {
            base.Init();

            meleeRangeDetector.OnMeleeRangeEnter += MeleeRangeEnter;
            meleeRangeDetector.OnMeleeRangeExit += MeleeRangeExit;
            TryAttack += Attack;
        }

        private IEnumerator AttackCooldown()
        {
            TryAttack -= Attack;
            yield return new WaitForSeconds(Unit.attackCD);
            TryAttack += Attack;
        }

        private void Attack()
        {
            TryAttack -= Attack;
            StartCoroutine(AttackCooldown());
            
            if (target == null) return;
            if (target.IsPoint) return;
            
            var unit = target.Entity as UnitVisual;
            if (unit == null) return;
            if (!unitsInRange.Contains(unit)) return;
            
            Debug.Log($"{gameObject.name} : Attacking {unit.name}");
            unit.TakeDamage();
        }
        
        protected override void UpdateAction()
        {
            base.UpdateAction();
            UpdateRotation();
        }
        
        private void UpdateRotation()
        {
            if (!isMoving && unitsInRange.Count > 0)
            {
                var unit = unitsInRange.First();
                var direction = unit.transform.position - transform.position;
                var rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
                return;
            }
            
            if (target == null) return;
            if (target.IsPoint) return;
            
            // var targetIsAlly = debugAsFriend; //Todo : check if target is ally
            //
            // if (targetIsAlly)
            // {
            //     var direction = target.Position - transform.position;
            //     var rotation = Quaternion.LookRotation(direction);
            //     transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
            //     return;
            // }

            if (unitsInRange.Contains(target.Entity))
            {
                var direction = target.Position - transform.position;
                var rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
        }
        
        protected override void UpdateDestination()
        {
            if (target == null) return;
            if (target.IsPoint) return;
                
            var targetIsAlly = IsAlly(Team, target.Entity.Team);
            var targetIsEnemy = IsEnemy(Team, target.Entity.Team);
            
            if (targetIsAlly)
            {
                SetDestination(target.Position);
                agent.stoppingDistance = followDistance;
                return;
            }

            if (targetIsEnemy && unitsInRange.Contains(target.Entity))
            {
                agent.stoppingDistance = attackDistance;
                SetDestination(target.Position);
                TryAttack?.Invoke();
                return;
            }
            SetDestination(target.Position);
        }

        [ContextMenu("TakeDamage")]
        public void TakeDamage()
        {
            OnUnitTakeDamage?.Invoke(this);
        }
        
        private void MeleeRangeEnter(Collider col)
        {
            Debug.Log($"{gameObject.name} : Melee range enter {col.name}");
            
            if (col.TryGetComponent(out UnitVisual unit))
            {
                UnitInRange(unit, true);
                OnUnitEnterMeleeRange?.Invoke(this, unit);
            }
        }
        
        private void MeleeRangeExit(Collider col)
        {
            Debug.Log($"{gameObject.name} : Melee range exit {col.name}");
            
            if (col.TryGetComponent(out UnitVisual unit))
            {
                UnitInRange(unit, false);
                OnUnitExitMeleeRange?.Invoke(this, unit);
            }
        }
        
        private void UnitInRange(UnitVisual unitVisual, bool enter)
        {
            bool isUnitInList = unitsInRange.Contains(unitVisual);
            
            if (enter && !isUnitInList)
            {
                unitsInRange.Add(unitVisual);
            }
            else if (!enter && isUnitInList)
            {
                unitsInRange.Remove(unitVisual);
            }
        }
    }

    [Serializable] public class Unit
    {
        [SerializeField] public float attackCD = 1f;
    }
}