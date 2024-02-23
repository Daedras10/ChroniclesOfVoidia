using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        
        public static event Action<RaycastHit> OnRaycastHit;
        
        private void Start()
        {
            InputsHandler.OnPrimaryClickStarted += ShootRay;
        }
        
        private void ShootRay(Vector2 mousePosition)
        {
            var ray = cam.ScreenPointToRay(mousePosition);
            var raycastHit = Physics.Raycast(ray, out var hit, Mathf.Infinity);
            
            Debug.DrawRay(ray.origin, ray.direction * 100, raycastHit ? Color.green : Color.red, 1f);
            
            if (!raycastHit) return;
            
            OnRaycastHit?.Invoke(hit);
        }
    }
}