using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    public class Unit : MonoBehaviour, ISelectable
    {
        [SerializeField] NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject selectionIndicator;
        
        [SerializeField] private float multiplier = 10f;

        private void Start()
        {
            Deselect();
        }
        
        private void Update()
        {
            var speed = agent.velocity.magnitude * multiplier;
            animator.SetFloat("speedv", speed);
        }

        public void SetDestination(Vector3 destination)
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