using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Raycaster : MonoBehaviour
    {
        private static Camera Cam;
        [SerializeField] private Camera cam;
        
        public static event Action<RaycastHit> OnRaycastHitPrimary;
        public static event Action<RaycastHit> OnRaycastHitSecondary;
        
        private void Start()
        {
            InputHandler.OnPrimaryClickStarted += ShootRay1;
            InputHandler.OnSecondaryClickStarted += ShootRay2;
        }

        private void OnDisable()
        {
            InputHandler.OnPrimaryClickStarted -= ShootRay1;
            InputHandler.OnSecondaryClickStarted -= ShootRay2;
        }
        
        private void ShootRay1(Vector2 mousePosition)
        {
            ShootRay(mousePosition, true);
        }
        
        private void ShootRay2(Vector2 mousePosition)
        {
            ShootRay(mousePosition, false);
        }

        private void ShootRay(Vector2 mousePosition, bool isPrimary)
        {
            var hasHit = ShootRay(mousePosition, out var hit, default, true);
            
            if (!hasHit) return;

            Debug.Log(hit.transform.name);
            
            if (isPrimary) {
                OnRaycastHitPrimary?.Invoke(hit);
                return;
            }
            
            OnRaycastHitSecondary?.Invoke(hit);
        }


        public static bool ShootRay(Vector2 mousePosition, out RaycastHit hit, LayerMask layerMask = default, bool debug = false)
        {
            if (Cam == null) Cam = Camera.main;
            
            var ray = Cam.ScreenPointToRay(mousePosition);
            var raycastHit = Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default"));
            
            if (debug) Debug.DrawRay(ray.origin, ray.direction * 100, raycastHit ? Color.green : Color.red, 1f);
            
            return raycastHit;
        }
    }
}