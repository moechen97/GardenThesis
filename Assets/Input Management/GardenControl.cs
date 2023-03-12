//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input Management/GardenControl.inputactions
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

public partial class @GardenControl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GardenControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GardenControl"",
    ""maps"": [
        {
            ""name"": ""Plant"",
            ""id"": ""bf469b8f-1da4-4ff7-a71a-ce628ee38576"",
            ""actions"": [
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""8364aa14-a984-4d90-bfc1-6a35d7896a82"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseMiddlePress"",
                    ""type"": ""Value"",
                    ""id"": ""4432f258-b66d-4ba9-82bd-30c49d201a0b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d8193663-c952-4a3f-b6f1-069c30081f61"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FingerAndMouseDrag"",
                    ""type"": ""Value"",
                    ""id"": ""09597d47-7deb-449c-ad01-42a79b60400f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FirstFingerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""97d0be76-fe16-44b2-890f-e03f8381f5d9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondaryFingerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""b5838c7e-bb74-4f89-ab91-0fe54256e729"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ScrollZoom"",
                    ""type"": ""Value"",
                    ""id"": ""fcb9d63c-81c8-400c-9a00-fa7ea927205e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FirstTouchInformation"",
                    ""type"": ""Value"",
                    ""id"": ""0a0f9ca8-82d9-4ea9-956b-e73105993b9a"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondTouchInformation"",
                    ""type"": ""Value"",
                    ""id"": ""1f38400a-daa1-4e68-9eab-043783b75ae3"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondaryTouchContact"",
                    ""type"": ""Button"",
                    ""id"": ""20b66b9d-4083-443f-aa35-85a9545969a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b92c5b28-52fd-43f3-87cb-9d73802bbecf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap(duration=0.2,pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""257170a2-c1b9-40f3-81a6-6fd5bb7151ee"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": ""Tap(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MouseDrag"",
                    ""id"": ""09bee3a9-2e0b-4d0f-8d06-5f41fd870e9b"",
                    ""path"": ""OneModifier"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMiddlePress"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""c5f2dc5b-14e8-4b2c-9eec-70ed9580d5f4"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMiddlePress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""1a34c1d3-70ce-4d76-9d5f-52da1c59589a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMiddlePress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5bcd98ed-22fd-46ea-96f3-69543f04f887"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold(duration=0.8,pressPoint=0.5)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b7dd9ee-81bc-44f0-95fd-90f3668f983f"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": ""Hold(duration=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MouseDrag"",
                    ""id"": ""ed8723b3-4475-4fcd-9e2f-f6906a572a84"",
                    ""path"": ""OneModifier"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FingerAndMouseDrag"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""7e51e3ca-a91a-4200-b485-24502a956493"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FingerAndMouseDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""a200707e-711a-4fd9-97fd-3746dc1849f8"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FingerAndMouseDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""FingerDrag"",
                    ""id"": ""c765716a-6203-4243-a8c2-cc058cbe311d"",
                    ""path"": ""OneModifier"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FingerAndMouseDrag"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""d903d396-1ddb-4ba0-9038-a40d949f9f16"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FingerAndMouseDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""71d0d440-1475-40ea-885a-31ed5a21984a"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FingerAndMouseDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""be8903ac-0f78-450b-bea5-f7563fc2351d"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstFingerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""454c6ee6-c0a1-43a7-a3af-3cce3d147a72"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryFingerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86cae1d2-ced5-4af3-bc8a-e52e2b2a3182"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""ScrollZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3b48bfb-6e55-4612-b6b3-0aa45dc4cf4a"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstTouchInformation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55663691-9724-4cf8-a045-4e6395dc748a"",
                    ""path"": ""<Touchscreen>/touch1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondTouchInformation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdbfa71a-c567-4a75-a027-67d3e12a79e4"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryTouchContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Plant
        m_Plant = asset.FindActionMap("Plant", throwIfNotFound: true);
        m_Plant_Tap = m_Plant.FindAction("Tap", throwIfNotFound: true);
        m_Plant_MouseMiddlePress = m_Plant.FindAction("MouseMiddlePress", throwIfNotFound: true);
        m_Plant_Hold = m_Plant.FindAction("Hold", throwIfNotFound: true);
        m_Plant_FingerAndMouseDrag = m_Plant.FindAction("FingerAndMouseDrag", throwIfNotFound: true);
        m_Plant_FirstFingerPosition = m_Plant.FindAction("FirstFingerPosition", throwIfNotFound: true);
        m_Plant_SecondaryFingerPosition = m_Plant.FindAction("SecondaryFingerPosition", throwIfNotFound: true);
        m_Plant_ScrollZoom = m_Plant.FindAction("ScrollZoom", throwIfNotFound: true);
        m_Plant_FirstTouchInformation = m_Plant.FindAction("FirstTouchInformation", throwIfNotFound: true);
        m_Plant_SecondTouchInformation = m_Plant.FindAction("SecondTouchInformation", throwIfNotFound: true);
        m_Plant_SecondaryTouchContact = m_Plant.FindAction("SecondaryTouchContact", throwIfNotFound: true);
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

    // Plant
    private readonly InputActionMap m_Plant;
    private IPlantActions m_PlantActionsCallbackInterface;
    private readonly InputAction m_Plant_Tap;
    private readonly InputAction m_Plant_MouseMiddlePress;
    private readonly InputAction m_Plant_Hold;
    private readonly InputAction m_Plant_FingerAndMouseDrag;
    private readonly InputAction m_Plant_FirstFingerPosition;
    private readonly InputAction m_Plant_SecondaryFingerPosition;
    private readonly InputAction m_Plant_ScrollZoom;
    private readonly InputAction m_Plant_FirstTouchInformation;
    private readonly InputAction m_Plant_SecondTouchInformation;
    private readonly InputAction m_Plant_SecondaryTouchContact;
    public struct PlantActions
    {
        private @GardenControl m_Wrapper;
        public PlantActions(@GardenControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tap => m_Wrapper.m_Plant_Tap;
        public InputAction @MouseMiddlePress => m_Wrapper.m_Plant_MouseMiddlePress;
        public InputAction @Hold => m_Wrapper.m_Plant_Hold;
        public InputAction @FingerAndMouseDrag => m_Wrapper.m_Plant_FingerAndMouseDrag;
        public InputAction @FirstFingerPosition => m_Wrapper.m_Plant_FirstFingerPosition;
        public InputAction @SecondaryFingerPosition => m_Wrapper.m_Plant_SecondaryFingerPosition;
        public InputAction @ScrollZoom => m_Wrapper.m_Plant_ScrollZoom;
        public InputAction @FirstTouchInformation => m_Wrapper.m_Plant_FirstTouchInformation;
        public InputAction @SecondTouchInformation => m_Wrapper.m_Plant_SecondTouchInformation;
        public InputAction @SecondaryTouchContact => m_Wrapper.m_Plant_SecondaryTouchContact;
        public InputActionMap Get() { return m_Wrapper.m_Plant; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlantActions set) { return set.Get(); }
        public void SetCallbacks(IPlantActions instance)
        {
            if (m_Wrapper.m_PlantActionsCallbackInterface != null)
            {
                @Tap.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnTap;
                @MouseMiddlePress.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnMouseMiddlePress;
                @MouseMiddlePress.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnMouseMiddlePress;
                @MouseMiddlePress.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnMouseMiddlePress;
                @Hold.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnHold;
                @Hold.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnHold;
                @Hold.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnHold;
                @FingerAndMouseDrag.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnFingerAndMouseDrag;
                @FingerAndMouseDrag.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnFingerAndMouseDrag;
                @FingerAndMouseDrag.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnFingerAndMouseDrag;
                @FirstFingerPosition.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnFirstFingerPosition;
                @FirstFingerPosition.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnFirstFingerPosition;
                @FirstFingerPosition.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnFirstFingerPosition;
                @SecondaryFingerPosition.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondaryFingerPosition;
                @SecondaryFingerPosition.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondaryFingerPosition;
                @SecondaryFingerPosition.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondaryFingerPosition;
                @ScrollZoom.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnScrollZoom;
                @ScrollZoom.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnScrollZoom;
                @ScrollZoom.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnScrollZoom;
                @FirstTouchInformation.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnFirstTouchInformation;
                @FirstTouchInformation.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnFirstTouchInformation;
                @FirstTouchInformation.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnFirstTouchInformation;
                @SecondTouchInformation.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondTouchInformation;
                @SecondTouchInformation.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondTouchInformation;
                @SecondTouchInformation.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondTouchInformation;
                @SecondaryTouchContact.started -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondaryTouchContact;
                @SecondaryTouchContact.performed -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondaryTouchContact;
                @SecondaryTouchContact.canceled -= m_Wrapper.m_PlantActionsCallbackInterface.OnSecondaryTouchContact;
            }
            m_Wrapper.m_PlantActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @MouseMiddlePress.started += instance.OnMouseMiddlePress;
                @MouseMiddlePress.performed += instance.OnMouseMiddlePress;
                @MouseMiddlePress.canceled += instance.OnMouseMiddlePress;
                @Hold.started += instance.OnHold;
                @Hold.performed += instance.OnHold;
                @Hold.canceled += instance.OnHold;
                @FingerAndMouseDrag.started += instance.OnFingerAndMouseDrag;
                @FingerAndMouseDrag.performed += instance.OnFingerAndMouseDrag;
                @FingerAndMouseDrag.canceled += instance.OnFingerAndMouseDrag;
                @FirstFingerPosition.started += instance.OnFirstFingerPosition;
                @FirstFingerPosition.performed += instance.OnFirstFingerPosition;
                @FirstFingerPosition.canceled += instance.OnFirstFingerPosition;
                @SecondaryFingerPosition.started += instance.OnSecondaryFingerPosition;
                @SecondaryFingerPosition.performed += instance.OnSecondaryFingerPosition;
                @SecondaryFingerPosition.canceled += instance.OnSecondaryFingerPosition;
                @ScrollZoom.started += instance.OnScrollZoom;
                @ScrollZoom.performed += instance.OnScrollZoom;
                @ScrollZoom.canceled += instance.OnScrollZoom;
                @FirstTouchInformation.started += instance.OnFirstTouchInformation;
                @FirstTouchInformation.performed += instance.OnFirstTouchInformation;
                @FirstTouchInformation.canceled += instance.OnFirstTouchInformation;
                @SecondTouchInformation.started += instance.OnSecondTouchInformation;
                @SecondTouchInformation.performed += instance.OnSecondTouchInformation;
                @SecondTouchInformation.canceled += instance.OnSecondTouchInformation;
                @SecondaryTouchContact.started += instance.OnSecondaryTouchContact;
                @SecondaryTouchContact.performed += instance.OnSecondaryTouchContact;
                @SecondaryTouchContact.canceled += instance.OnSecondaryTouchContact;
            }
        }
    }
    public PlantActions @Plant => new PlantActions(this);
    public interface IPlantActions
    {
        void OnTap(InputAction.CallbackContext context);
        void OnMouseMiddlePress(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnFingerAndMouseDrag(InputAction.CallbackContext context);
        void OnFirstFingerPosition(InputAction.CallbackContext context);
        void OnSecondaryFingerPosition(InputAction.CallbackContext context);
        void OnScrollZoom(InputAction.CallbackContext context);
        void OnFirstTouchInformation(InputAction.CallbackContext context);
        void OnSecondTouchInformation(InputAction.CallbackContext context);
        void OnSecondaryTouchContact(InputAction.CallbackContext context);
    }
}
