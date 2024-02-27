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
        
        private void Start()
        {
            Deselect();
            
            meleeRangeDetector.OnMeleeRangeEnter += (collider) =>
            {
                Debug.Log($"{gameObject.name} : Melee range enter {collider.name}");
            };
            meleeRangeDetector.OnMeleeRangeExit += (collider) =>
            {
                Debug.Log($"{gameObject.name} : Melee range exit {collider.name}");
            };
        }
        
        private void Update()
        {
            var speed = agent.velocity.magnitude * multiplier;
            animator.SetFloat("speedv", speed);
            
            if (target == null) return;
            if (target.IsPoint) return;
            
            SetDestination(target.Position);
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