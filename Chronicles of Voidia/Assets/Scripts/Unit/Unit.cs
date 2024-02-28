using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleMap;
using Interfaces;
using Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Unit
{
    public class Unit : MonoBehaviour, ISelectable
    {
        private static readonly int Speedv = Animator.StringToHash("speedv");
        
        
        [Header("Components")]
        [SerializeField] NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject selectionIndicator;
        [SerializeField] private Renderer rendererRef;
        [Space]
        [SerializeField] private MeleeRangeDetector meleeRangeDetector;
        
        [Header("Settings")]
        [SerializeField] private float multiplier = 10f;
        [SerializeField] private float blinkWaitTime = 0.5f;
        [SerializeField] private float followDistance = 1f;
        [SerializeField] private float attackCD = 1f;
        [Space]
        [SerializeField] private float baseSpeed = 5f;

        [SerializeField] private bool debugAsFriend = false;

        private Target target;
        private Color originalColor;
        private List<Unit> unitsInRange = new ();
        private List<MovementCost> movementCosts = new ();
        
        
        private event Action TryAttack;
        
        public static event Action<Unit,Unit> OnUnitEnterMeleeRange;
        public static event Action<Unit,Unit> OnUnitExitMeleeRange;
        
        private void Start()
        {
            Deselect();

            meleeRangeDetector.OnMeleeRangeEnter += MeleeRangeEnter;
            meleeRangeDetector.OnMeleeRangeExit += MeleeRangeExit;
            TryAttack += Attack;
            
            originalColor = rendererRef.material.color;
            UpdateSpeed();
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
            
            var unit = target.Entity;
            if (unitsInRange.Contains(unit))
            {
                Debug.Log($"{gameObject.name} : Attacking {unit.name}");
                unit.BlinkDamage();
            }
        }
        
        private void Update()
        {
            UpdateDestination();
            UpdateAnimationSpeed();
            
            return;
            
            void UpdateAnimationSpeed()
            {
                var speed = agent.velocity.magnitude * multiplier;
                animator.SetFloat(Speedv, speed);
            }
        
            void UpdateDestination()
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
            
            if (col.TryGetComponent(out Unit unit))
            {
                UnitInRange(unit, true);
                OnUnitEnterMeleeRange?.Invoke(this, unit);
            }
        }
        
        private void MeleeRangeExit(Collider col)
        {
            Debug.Log($"{gameObject.name} : Melee range exit {col.name}");
            
            if (col.TryGetComponent(out Unit unit))
            {
                UnitInRange(unit, false);
                OnUnitExitMeleeRange?.Invoke(this, unit);
            }
        }
        
        private void UnitInRange(Unit unit, bool enter)
        {
            bool isUnitInList = unitsInRange.Contains(unit);
            
            if (enter && !isUnitInList)
            {
                unitsInRange.Add(unit);
            }
            else if (!enter && isUnitInList)
            {
                unitsInRange.Remove(unit);
            }
        }
        
        public void SetTarget(Target tgt)
        {
            target = tgt;
            agent.SetDestination(target.Position);
        }
        
        private void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void Select()
        {
            selectionIndicator.SetActive(true);
        }

        public void Deselect()
        {
            selectionIndicator.SetActive(false);
        }

        [ContextMenu("UpdateSpeed")]
        private void UpdateSpeed()
        {
            var speed = baseSpeed;
            
            if (movementCosts.Count > 0)
            {
                var maxCost = movementCosts.Max(mc => mc.Cost);
                speed = baseSpeed / maxCost;
            }
            
            agent.speed = speed;
        }
        
        public void AddDifficultTerrain(DifficultTerrain terrain, float cost)
        {
            if (movementCosts.Exists(mc => mc.Terrain == terrain)) return;
            
            var movementCost = new MovementCost(terrain, cost);
            movementCosts.Add(movementCost);
            
            UpdateSpeed();
        }
        
        public void RemoveDifficultTerrain(DifficultTerrain terrain)
        {
            var movementCost = movementCosts.Find(mc => mc.Terrain == terrain);
            if (movementCost == null) return;
            
            movementCosts.Remove(movementCost);
            
            UpdateSpeed();
        }
    }
}