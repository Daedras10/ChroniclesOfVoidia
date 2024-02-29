using BattleEntity;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private UnitVisual _unitVisual;
    
    private void OnEnable()
    {
        Raycaster.OnRaycastHitPrimary += Test;
    }
    
    private void OnDisable()
    {
        Raycaster.OnRaycastHitPrimary -= Test;
    }

    private void Test(RaycastHit hit)
    {
        //unit.SetDestination(hit.point);
    }
}
