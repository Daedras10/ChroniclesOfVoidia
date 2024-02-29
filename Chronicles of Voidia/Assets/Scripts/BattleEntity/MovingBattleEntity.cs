using System.Collections.Generic;
using System.Linq;
using BattleMap;
using UnityEngine;
using UnityEngine.AI;

namespace BattleEntity
{
    public class MovingBattleEntity : BattleEntity
    {
        [Header("Components")]
        [SerializeField] protected NavMeshAgent agent;
        
        [Header("Settings")]
        [SerializeField] protected float baseSpeed = 5f;
        [SerializeField] protected float followDistance = 1.8f;

        protected Target target;
        protected List<MovementCost> movementCosts = new ();
        
        protected override void Init()
        {
            base.Init();
            UpdateSpeed();
        }
        
        protected override void UpdateAction()
        {
            UpdateDestination();
        }
        
        protected virtual void UpdateDestination()
        {
            if (target == null) return;
            if (target.IsPoint) return;
            
            SetDestination(target.Position);
            agent.stoppingDistance = followDistance;
        }
        
        public void SetTarget(Target tgt)
        {
            target = tgt;
            agent.SetDestination(target.Position);
        }
        
        protected void SetDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        [ContextMenu("UpdateSpeed")]
        private void UpdateSpeed()
        {
            var speed = baseSpeed;
            
            if (movementCosts.Count > 0)
            {
                var maxCost = movementCosts.Max(mc => mc.Cost);
                speed = baseSpeed / maxCost;
            }
            
            agent.speed = speed;
        }
        
        public void AddDifficultTerrain(DifficultTerrain terrain, float cost)
        {
            if (movementCosts.Exists(mc => mc.Terrain == terrain)) return;
            
            var movementCost = new MovementCost(terrain, cost);
            movementCosts.Add(movementCost);
            
            UpdateSpeed();
        }
        
        public void RemoveDifficultTerrain(DifficultTerrain terrain)
        {
            var movementCost = movementCosts.Find(mc => mc.Terrain == terrain);
            if (movementCost == null) return;
            
            movementCosts.Remove(movementCost);
            
            UpdateSpeed();
        }
    }
}