using BattleEntity;
using UnityEngine;

namespace BattleMap
{
    public class DifficultTerrain : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private BoxCollider boxCollider;
        
        [Header("Settings")]
        [SerializeField] private float movementCost = 2f;
        [SerializeField] private Vector3 size = new Vector3(1, 1, 1);
        
        [ContextMenu("Update Collider Size")]
        private void UpdateColliderSize()
        {
            boxCollider.size = size;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var movingBattleEntity = other.gameObject.GetComponent<MovingBattleEntity>();
            if ( movingBattleEntity == null) return;
            
            movingBattleEntity.AddDifficultTerrain(this, movementCost);
        }
        
        private void OnTriggerExit(Collider other)
        {
            var movingBattleEntity = other.gameObject.GetComponent<MovingBattleEntity>();
            if ( movingBattleEntity == null) return;
            
            movingBattleEntity.RemoveDifficultTerrain(this);
        }
    }
    
    public class MovementCost
    {
        public DifficultTerrain Terrain { get; }
        public float Cost { get; }
        
        public MovementCost(DifficultTerrain terrain, float cost)
        {
            Terrain = terrain;
            Cost = cost;
        }
    }
}