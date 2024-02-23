//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/Inputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Inputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Battle"",
            ""id"": ""dd5deb8d-d36a-436f-9aaa-881514b0f4c9"",
            ""actions"": [
                {
                    ""name"": ""PrimaryClick"",
                    ""type"": ""Button"",
                    ""id"": ""680f35ae-c8f2-4757-b5d6-bc6aa1cc6df8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryClick"",
                    ""type"": ""Button"",
                    ""id"": ""4bf9d437-10cc-4490-890e-2bf32302f19a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""Value"",
                    ""id"": ""2009e30a-703b-4318-9188-6e3fe1d6cc13"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseZoom"",
                    ""type"": ""Value"",
                    ""id"": ""a1f1f274-3db3-40c3-9fe4-d3c54b7f10fa"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseWheel"",
                    ""type"": ""Button"",
                    ""id"": ""fff02ec1-6a02-4ce7-b1b0-fdfbb9a8a685"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""087a67a7-2dd9-4183-8295-6b559263beb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a3643e4f-424e-44d9-87c4-4d8e5541484d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28b21ad9-ecfd-4f96-af29-2bc408bb5c78"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a07b3a72-f29c-4430-8821-6a0f9dc5423c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a462f29b-19a5-4b63-928e-93276efc668f"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""0cfef0f3-0c3e-429d-bc42-04ba4cce7668"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""72a9bc99-c9ee-41bb-8c05-266ce8b7c141"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c7ff01bc-0c59-43b4-b704-75b47f5717e8"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""76b697b2-4fa2-42b3-851c-79fe8246e437"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Battle
        m_Battle = asset.FindActionMap("Battle", throwIfNotFound: true);
        m_Battle_PrimaryClick = m_Battle.FindAction("PrimaryClick", throwIfNotFound: true);
        m_Battle_SecondaryClick = m_Battle.FindAction("SecondaryClick", throwIfNotFound: true);
        m_Battle_MouseMove = m_Battle.FindAction("MouseMove", throwIfNotFound: true);
        m_Battle_MouseZoom = m_Battle.FindAction("MouseZoom", throwIfNotFound: true);
        m_Battle_MouseWheel = m_Battle.FindAction("MouseWheel", throwIfNotFound: true);
        m_Battle_Shift = m_Battle.FindAction("Shift", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Battle
    private readonly InputActionMap m_Battle;
    private List<IBattleActions> m_BattleActionsCallbackInterfaces = new List<IBattleActions>();
    private readonly InputAction m_Battle_PrimaryClick;
    private readonly InputAction m_Battle_SecondaryClick;
    private readonly InputAction m_Battle_MouseMove;
    private readonly InputAction m_Battle_MouseZoom;
    private readonly InputAction m_Battle_MouseWheel;
    private readonly InputAction m_Battle_Shift;
    public struct BattleActions
    {
        private @Inputs m_Wrapper;
        public BattleActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryClick => m_Wrapper.m_Battle_PrimaryClick;
        public InputAction @SecondaryClick => m_Wrapper.m_Battle_SecondaryClick;
        public InputAction @MouseMove => m_Wrapper.m_Battle_MouseMove;
        public InputAction @MouseZoom => m_Wrapper.m_Battle_MouseZoom;
        public InputAction @MouseWheel => m_Wrapper.m_Battle_MouseWheel;
        public InputAction @Shift => m_Wrapper.m_Battle_Shift;
        public InputActionMap Get() { return m_Wrapper.m_Battle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleActions set) { return set.Get(); }
        public void AddCallbacks(IBattleActions instance)
        {
            if (instance == null || m_Wrapper.m_BattleActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BattleActionsCallbackInterfaces.Add(instance);
            @PrimaryClick.started += instance.OnPrimaryClick;
            @PrimaryClick.performed += instance.OnPrimaryClick;
            @PrimaryClick.canceled += instance.OnPrimaryClick;
            @SecondaryClick.started += instance.OnSecondaryClick;
            @SecondaryClick.performed += instance.OnSecondaryClick;
            @SecondaryClick.canceled += instance.OnSecondaryClick;
            @MouseMove.started += instance.OnMouseMove;
            @MouseMove.performed += instance.OnMouseMove;
            @MouseMove.canceled += instance.OnMouseMove;
            @MouseZoom.started += instance.OnMouseZoom;
            @MouseZoom.performed += instance.OnMouseZoom;
            @MouseZoom.canceled += instance.OnMouseZoom;
            @MouseWheel.started += instance.OnMouseWheel;
            @MouseWheel.performed += instance.OnMouseWheel;
            @MouseWheel.canceled += instance.OnMouseWheel;
            @Shift.started += instance.OnShift;
            @Shift.performed += instance.OnShift;
            @Shift.canceled += instance.OnShift;
        }

        private void UnregisterCallbacks(IBattleActions instance)
        {
            @PrimaryClick.started -= instance.OnPrimaryClick;
            @PrimaryClick.performed -= instance.OnPrimaryClick;
            @PrimaryClick.canceled -= instance.OnPrimaryClick;
            @SecondaryClick.started -= instance.OnSecondaryClick;
            @SecondaryClick.performed -= instance.OnSecondaryClick;
            @SecondaryClick.canceled -= instance.OnSecondaryClick;
            @MouseMove.started -= instance.OnMouseMove;
            @MouseMove.performed -= instance.OnMouseMove;
            @MouseMove.canceled -= instance.OnMouseMove;
            @MouseZoom.started -= instance.OnMouseZoom;
            @MouseZoom.performed -= instance.OnMouseZoom;
            @MouseZoom.canceled -= instance.OnMouseZoom;
            @MouseWheel.started -= instance.OnMouseWheel;
            @MouseWheel.performed -= instance.OnMouseWheel;
            @MouseWheel.canceled -= instance.OnMouseWheel;
            @Shift.started -= instance.OnShift;
            @Shift.performed -= instance.OnShift;
            @Shift.canceled -= instance.OnShift;
        }

        public void RemoveCallbacks(IBattleActions instance)
        {
            if (m_Wrapper.m_BattleActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBattleActions instance)
        {
            foreach (var item in m_Wrapper.m_BattleActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BattleActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BattleActions @Battle => new BattleActions(this);
    public interface IBattleActions
    {
        void OnPrimaryClick(InputAction.CallbackContext context);
        void OnSecondaryClick(InputAction.CallbackContext context);
        void OnMouseMove(InputAction.CallbackContext context);
        void OnMouseZoom(InputAction.CallbackContext context);
        void OnMouseWheel(InputAction.CallbackContext context);
        void OnShift(InputAction.CallbackContext context);
    }
}
