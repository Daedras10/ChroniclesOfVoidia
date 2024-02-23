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
    

    private void OnDisable()
    {
        inputs.Disable();
    }
}
