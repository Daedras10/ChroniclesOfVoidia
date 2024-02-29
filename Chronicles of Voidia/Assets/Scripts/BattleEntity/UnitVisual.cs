using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleEntity
{
    public class UnitVisual : MovingBattleEntity
    {
        private static readonly int Speedv = Animator.StringToHash("speedv");
        
        
        [Header("Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private Renderer rendererRef;
        [Space]
        [SerializeField] private MeleeRangeDetector meleeRangeDetector;
        
        [Header("Settings")]
        [SerializeField] private float animationSpeedMult = 10f;
        [SerializeField] private float blinkWaitTime = 0.5f;
        [SerializeField] private float attackCD = 1f;
        [SerializeField] private bool debugAsFriend = false;

        private Color originalColor;
        private List<UnitVisual> unitsInRange = new ();
        
        
        private event Action TryAttack;
        public static event Action<UnitVisual,UnitVisual> OnUnitEnterMeleeRange;
        public static event Action<UnitVisual,UnitVisual> OnUnitExitMeleeRange;
        
        protected override void Init()
        {
            base.Init();

            meleeRangeDetector.OnMeleeRangeEnter += MeleeRangeEnter;
            meleeRangeDetector.OnMeleeRangeExit += MeleeRangeExit;
            TryAttack += Attack;
            
            originalColor = rendererRef.material.color;
        }

        private IEnumerator AttackCooldown()
            {
                TryAttack -= Attack;
                yield return new WaitForSeconds(attackCD);
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
            unit.BlinkDamage();
        }
        
        protected override void UpdateAction()
        {
            base.UpdateAction();
            UpdateAnimationSpeed();
        }
        
        private void UpdateAnimationSpeed()
        {
            var speed = agent.velocity.magnitude * animationSpeedMult;
            animator.SetFloat(Speedv, speed);
        }
        
        protected override void UpdateDestination()
        {
            if (target == null) return;
            if (target.IsPoint) return;
                
            var targetIsAlly = debugAsFriend; //Todo : check if target is ally
            
            if (targetIsAlly)
            {
                SetDestination(target.Position);
                agent.stoppingDistance = followDistance;
                return;
            }

            if (unitsInRange.Contains(target.Entity))
            {
                SetDestination(transform.position);
                TryAttack?.Invoke();
                return;
            }
            SetDestination(target.Position);
        }

        [ContextMenu("Blink Damage")]
        public void BlinkDamage()
        {
            StartCoroutine(BlinkCoroutine());
            return;
            
            IEnumerator BlinkCoroutine()
            {
                var mat = rendererRef.material;
                
                mat.color = Color.red;
                yield return new WaitForSeconds(blinkWaitTime);
                mat.color = originalColor;
            }
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
}