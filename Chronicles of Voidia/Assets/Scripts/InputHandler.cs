using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static Vector2 MousePosition { get; private set; }
    public static event Action<Vector2> OnPrimaryClickStarted;
    public static event Action<Vector2> OnPrimaryClickTiggered;
    public static event Action<Vector2> OnPrimaryClickEnded;
    public static event Action<Vector2> OnSecondaryClickStarted;
    public static event Action<Vector2> OnSecondaryClickTiggered;
    public static event Action<Vector2> OnSecondaryClickEnded;
    public static event Action<Vector2> OnMouseMoved;
    public static event Action OnShiftStarted;
    public static event Action OnShiftEnded;
    public static event Action OnLAltStarted;
    public static event Action OnLAltEnded;
    public static event Action OnAStarted;
    public static event Action OnTabStarted;
    public static event Action OnTabEnded;
    public static event Action OnTabPerformed;
    
    
    
    private Inputs inputs;
    
    private bool primaryClickStarted = false;
    private bool secondaryClickStarted = false;

    private void Awake()
    {
        CreateInputs();
    }
    
    private void CreateInputs()
    {
        inputs = new Inputs();
        
        inputs.Battle.PrimaryClick.started += PrimaryClickStarted;
        inputs.Battle.PrimaryClick.canceled += PrimaryClickEnded;
        inputs.Battle.SecondaryClick.started += SecondaryClickStarted;
        inputs.Battle.SecondaryClick.canceled += SecondaryClickEnded;
        inputs.Battle.MouseMove.performed += MouseMoved;

        inputs.Battle.Shift.started += ShiftStarted;
        inputs.Battle.Shift.canceled += ShiftEnded;
        
        inputs.Battle.LAlt.started += LAltStarted;
        inputs.Battle.LAlt.canceled += LAltEnded;
        
        inputs.Battle.A.started += AStarted;
        inputs.Battle.A.canceled += AEnded;
        
        inputs.Battle.Tab.started += TabStarted;
        inputs.Battle.Tab.canceled += TabEnded;
        inputs.Battle.Tab.performed += TabPerformed;
        
        inputs.Enable();
        
        MousePosition = inputs.Battle.MouseMove.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (primaryClickStarted) OnPrimaryClickTiggered?.Invoke(MousePosition);
        if (secondaryClickStarted) OnSecondaryClickTiggered?.Invoke(MousePosition);
    }
    
    private void RefreshMousePosition(InputAction.CallbackContext ctx)
    {
        MousePosition = ctx.ReadValue<Vector2>();
    }
    
    private void PrimaryClickStarted(InputAction.CallbackContext ctx)
    {
        OnPrimaryClickStarted?.Invoke(MousePosition);
        primaryClickStarted = true;
    }
    
    private void PrimaryClickEnded(InputAction.CallbackContext ctx)
    {
        OnPrimaryClickEnded?.Invoke(MousePosition);
        primaryClickStarted = false;
    }
    
    private void SecondaryClickStarted(InputAction.CallbackContext ctx)
    {
        OnSecondaryClickStarted?.Invoke(MousePosition);
        secondaryClickStarted = true;
    }
    
    private void SecondaryClickEnded(InputAction.CallbackContext ctx)
    {
        OnSecondaryClickEnded?.Invoke(MousePosition);
        secondaryClickStarted = false;
    }
    
    private void MouseMoved(InputAction.CallbackContext ctx)
    {
        RefreshMousePosition(ctx);
        OnMouseMoved?.Invoke(MousePosition);
    }
    
    private void ShiftStarted(InputAction.CallbackContext ctx)
    {
        OnShiftStarted?.Invoke();
    }
    
    private void ShiftEnded(InputAction.CallbackContext ctx)
    {
        OnShiftEnded?.Invoke();
    }
    
    private void LAltStarted(InputAction.CallbackContext ctx)
    {
        OnLAltStarted?.Invoke();
    }
    
    private void LAltEnded(InputAction.CallbackContext ctx)
    {
        OnLAltEnded?.Invoke();
    }
    
    private void AStarted(InputAction.CallbackContext ctx)
    {
        OnAStarted?.Invoke();
    }
    
    private void AEnded(InputAction.CallbackContext ctx)
    {
        OnAStarted?.Invoke();
    }
    
    private void TabStarted(InputAction.CallbackContext ctx)
    {
        OnTabStarted?.Invoke();
    }
    
    private void TabEnded(InputAction.CallbackContext ctx)
    {
        OnTabEnded?.Invoke();
    }
    
    private void TabPerformed(InputAction.CallbackContext ctx)
    {
        OnTabPerformed?.Invoke();
    }
    

    private void OnDisable()
    {
        inputs.Disable();
    }
}
