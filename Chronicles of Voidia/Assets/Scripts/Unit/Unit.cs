using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;
        
        public void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
    }
}