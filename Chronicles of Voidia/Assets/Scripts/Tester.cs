using System;
using DefaultNamespace;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private Unit.Unit unit;
    
    private void OnEnable()
    {
        Raycaster.OnRaycastHit += Test;
    }
    
    private void OnDisable()
    {
        Raycaster.OnRaycastHit -= Test;
    }

    private void Test(RaycastHit hit)
    {
        unit.SetDestination(hit.point);
    }
}
