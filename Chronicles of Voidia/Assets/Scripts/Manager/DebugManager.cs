using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Manager
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField] private DebugLineRenderer debugLineRendererPrefab;
        
        private List<AimCombo> lineCombos = new ();
        
        private void OnEnable()
        {
            Unit.Unit.OnUnitEnterMeleeRange += AddDebugLineRenderer;
            Unit.Unit.OnUnitExitMeleeRange += RemoveFromDebugLineRenderer;
        }
        
        private void OnDisable()
        {
            Unit.Unit.OnUnitEnterMeleeRange -= AddDebugLineRenderer;
            Unit.Unit.OnUnitExitMeleeRange -= RemoveFromDebugLineRenderer;
        }
        
        private void AddDebugLineRenderer(Unit.Unit entity1, Unit.Unit entity2)
        {
            var alreadyPresent = lineCombos.Where(lc =>
                    (lc.Entity1 == entity1 && lc.Entity2 == entity2) ||
                    (lc.Entity1 == entity1 && lc.Entity2 == entity2))
                .ToList().Count > 0;
            
            if (alreadyPresent) return;
            
            
            var debugLineRenderer = Instantiate(debugLineRendererPrefab);
            debugLineRenderer.Init(entity1.gameObject, entity2.gameObject);
            
            var lineCombo = new AimCombo(entity1, entity2, debugLineRenderer);
            
            lineCombos.Add(lineCombo);
        }
        
        private void RemoveFromDebugLineRenderer(Unit.Unit entity1, Unit.Unit entity2)
        {
            var lineCombo = lineCombos.FirstOrDefault(lc =>
                (lc.Entity1 == entity1 && lc.Entity2 == entity2) ||
                (lc.Entity1 == entity1 && lc.Entity2 == entity2));
            
            if (lineCombo == null) return;
            
            lineCombos.Remove(lineCombo);
            lineCombo.DebugLineRenderer.Destroy();
        }
    }
    
    public class AimCombo
    {
        public Unit.Unit Entity1;
        public Unit.Unit Entity2;
        public DebugLineRenderer DebugLineRenderer;

        public AimCombo(Unit.Unit entity1, Unit.Unit entity2, DebugLineRenderer debugLineRenderer)
        {
            Entity1 = entity1;
            Entity2 = entity2;
            DebugLineRenderer = debugLineRenderer;
        }
    }
}