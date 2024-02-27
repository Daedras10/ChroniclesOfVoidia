using System;
using Interfaces;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    public class Unit : MonoBehaviour, ISelectable
    {
        [Header("Components")]
        [SerializeField] NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject selectionIndicator;
        [Space]
        [SerializeField] private MeleeRangeDetector meleeRangeDetector;
        
        [Header("Settings")]
        [SerializeField] private float multiplier = 10f;

        private Target target;
        
        public static event Action<Unit,Unit> OnUnitEnterMeleeRange;
        public static event Action<Unit,Unit> OnUnitExitMeleeRange;
        
        private void Start()
        {
            Deselect();

            meleeRangeDetector.OnMeleeRangeEnter += MeleeRangeEnter;
            meleeRangeDetector.OnMeleeRangeExit += MeleeRangeExit;
        }
        
        private void Update()
        {
            var speed = agent.velocity.magnitude * multiplier;
            animator.SetFloat("speedv", speed);
            
            if (target == null) return;
            if (target.IsPoint) return;
            
            SetDestination(target.Position);
        }
        
        private void MeleeRangeEnter(Collider collider)
        {
            Debug.Log($"{gameObject.name} : Melee range enter {collider.name}");
            
            if (collider.TryGetComponent(out Unit unit))
            {
                OnUnitEnterMeleeRange?.Invoke(this, unit);
            }
        }
        
        private void MeleeRangeExit(Collider collider)
        {
            Debug.Log($"{gameObject.name} : Melee range exit {collider.name}");
            
            if (collider.TryGetComponent(out Unit unit))
            {
                OnUnitExitMeleeRange?.Invoke(this, unit);
            }
        }

        
        public void SetTarget(Target target)
        {
            this.target = target;
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
    }
}