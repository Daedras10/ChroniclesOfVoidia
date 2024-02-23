using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<Unit.Unit> units = new ();
    private Vector2 selectionMouseStart;
    private bool dragSelection;
    private bool shiftIsPressed;

    public static event Action<Vector2, Vector2, bool> DrawSelectionBox;
    
    // Start is called before the first frame update
    void Start()
    {
        units = new List<Unit.Unit>();
    }
    
    private void OnEnable()
    {
        InputHandler.OnPrimaryClickStarted += SelectionStart;
        InputHandler.OnPrimaryClickEnded += SelectionEnd;
        Raycaster.OnRaycastHitSecondary += MoveUnitsTo;
    }
    
    private void OnDisable()
    {
        InputHandler.OnPrimaryClickStarted -= SelectionStart;
        InputHandler.OnPrimaryClickEnded -= SelectionEnd;
        Raycaster.OnRaycastHitSecondary -= MoveUnitsTo;
        
        InputHandler.OnMouseMoved -= CheckForDrag;
        InputHandler.OnMouseMoved -= UpdateDrag;
    }

    
    private void SelectionStart(Vector2 position)
    {
        // Save the start position of the selection box from mouse to world
        selectionMouseStart = position;
        dragSelection = false;
        
        if (!shiftIsPressed) ClearSelection();
        
        InputHandler.OnMouseMoved += CheckForDrag;
    }

    private void CheckForDrag(Vector2 position)
    {
        if (Vector2.Distance(selectionMouseStart, position) < 40f) return;
        
        dragSelection = true;
        InputHandler.OnMouseMoved -= CheckForDrag;
        InputHandler.OnMouseMoved += UpdateDrag;
    }
    
    private void UpdateDrag(Vector2 position)
    {
        if (!dragSelection) return;
        
        DrawSelectionBox?.Invoke(selectionMouseStart, position, true);
        TryAddUnitsFromSelection(position);
    }

    private RaycastHit corner1;
    private RaycastHit corner2;
    
    private void TryAddUnitsFromSelection(Vector2 endPosition)
    {
        var min = new Vector2(Mathf.Min(selectionMouseStart.x, endPosition.x), Mathf.Min(selectionMouseStart.y, endPosition.y));
        var max = new Vector2(Mathf.Max(selectionMouseStart.x, endPosition.x), Mathf.Max(selectionMouseStart.y, endPosition.y));
        
        var corner1Hit = Raycaster.ShootRay(min, out corner1, 1 << 8, true);
        var corner2Hit = Raycaster.ShootRay(max, out corner2, 1 << 8, true);
        
        if (!corner1Hit || !corner2Hit) return;
        
        var unitsInBox = Physics.OverlapBox((corner1.point + corner2.point) / 2, new Vector3(Mathf.Abs(corner1.point.x - corner2.point.x), 0.1f, Mathf.Abs(corner1.point.z - corner2.point.z)));
        
        
        ClearSelection();
        
        foreach (var raycastHit in unitsInBox)
        {
            Debug.Log(raycastHit.transform.name);
            var unit = raycastHit.transform.GetComponent<Unit.Unit>();
            if (unit == null) continue;
            if (units.Contains(unit)) continue;

            Debug.Log($"{raycastHit.transform.name} added to selection {raycastHit}");
            
            units.Add(unit);
            unit.Select();
        }
        

        Debug.Log($"units selected {units.Count} among {unitsInBox.Length} potential units in the box.");
        
    }
    
    private void SelectionEnd(Vector2 position)
    {
        InputHandler.OnMouseMoved -= CheckForDrag;
        InputHandler.OnMouseMoved -= UpdateDrag;

        if (!dragSelection)
        {
            // Check Raycast & Select if unit
        }
        
        DrawSelectionBox?.Invoke(selectionMouseStart, position, false);
        
        dragSelection = false;
        return;
        var selectionStart = Camera.main.ScreenToWorldPoint(selectionMouseStart);
        var selectionEnd = Camera.main.ScreenToWorldPoint(position);
        
        var raycastHits = Physics.OverlapBox((selectionStart + selectionEnd) / 2, new Vector3(Mathf.Abs(selectionStart.x - selectionEnd.x), Mathf.Abs(selectionStart.y - selectionEnd.y), 1));
        
        
    }
    
    private void ClearSelection()
    {
        foreach (var unit in units)
        {
            if (unit == null) continue;
            unit.Deselect();
        }
        units.Clear();
    }
    
    private void MoveUnitsTo(RaycastHit hit)
    {
        foreach (var unit in units)
        {
            unit.SetDestination(hit.point);
        }
    }

    private void OnDrawGizmos()
    {
        if (!dragSelection) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((corner1.point + corner2.point) / 2, new Vector3(Mathf.Abs(corner1.point.x - corner2.point.x), 1,Mathf.Abs(corner1.point.z - corner2.point.z)));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(corner1.point, 0.5f);
        Gizmos.DrawWireSphere(corner2.point, 0.5f);
        /*
        //Draw a debug box from corner1 to corner2
        Debug.DrawLine(corner1.point, new Vector3(corner1.point.x, corner2.point.y, corner1.point.z), Color.red, 1f);
        Debug.DrawLine(corner1.point, new Vector3(corner2.point.x, corner1.point.y, corner1.point.z), Color.red, 1f);
        Debug.DrawLine(corner2.point, new Vector3(corner1.point.x, corner2.point.y, corner1.point.z), Color.red, 1f);
        Debug.DrawLine(corner2.point, new Vector3(corner2.point.x, corner1.point.y, corner1.point.z), Color.red, 1f);
         */
    }
}
