using System.Collections;
using UnityEngine;

namespace BattleEntity
{
    public class UnitAnimation : MonoBehaviour
    {
        private static readonly int Speedv = Animator.StringToHash("speedv");
        
        [Header("Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private Renderer rendererRef;
        [SerializeField] private UnitVisual unitVisual;
        [SerializeField] protected UnityEngine.AI.NavMeshAgent agent;
        
        [Header("Settings")]
        [SerializeField] private float blinkWaitTime = 0.5f;
        [SerializeField] private float animationSpeedMult = 10f;
        
        private Color originalColor;
        
        private void Start()
        {
            originalColor = rendererRef.material.color;
            unitVisual.OnUnitTakeDamage += BlinkOnDamage;
        }
        
        private void Update()
        {
            UpdateAnimationSpeed();
        }
        
        private void UpdateAnimationSpeed()
        {
            var speed = agent.velocity.magnitude * animationSpeedMult;
            animator.SetFloat(Speedv, speed);
        }
        
        private void BlinkOnDamage(UnitVisual _)
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
    }
}