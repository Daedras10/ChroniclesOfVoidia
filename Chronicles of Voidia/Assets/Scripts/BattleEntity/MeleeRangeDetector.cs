using System;
using UnityEngine;

namespace BattleEntity
{
    public class MeleeRangeDetector : MonoBehaviour
    {
        public event Action<Collider> OnMeleeRangeEnter; // Another type of object ?
        public event Action<Collider> OnMeleeRangeExit;
        
        private void OnTriggerEnter(Collider other)
        {
            OnMeleeRangeEnter?.Invoke(other);
            Debug.Log("Melee range enter");
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnMeleeRangeExit?.Invoke(other);
        }
    }
}