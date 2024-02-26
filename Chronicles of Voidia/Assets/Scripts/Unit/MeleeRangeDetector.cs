using System;
using UnityEngine;

namespace Unit
{
    public class MeleeRangeDetector : MonoBehaviour
    {
        public event Action<Collider> OnMeleeRangeEnter; // Another type of object ?
        public event Action<Collider> OnMeleeRangeExit;
        
        private void OnTriggerEnter(Collider other)
        {
            OnMeleeRangeEnter?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnMeleeRangeExit?.Invoke(other);
        }
    }
}