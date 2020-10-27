using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;

public class ControllerSimulator : MonoBehaviour
{
#if UNITY_EDITOR
    public float controllerDefaultDistance = 1;
    public float controllerMaxDistance = 2;
    public float controllerMinDistance = 0;
    public float scrollWheelToDistance = 0.1f;
    public KeyCode selectKey = KeyCode.Mouse1;
    public KeyCode activateKey = KeyCode.Return;
    public KeyCode triggerKey = KeyCode.Return;
    public KeyCode gripKey = KeyCode.Mouse1;
    public KeyCode switchControllerKey = KeyCode.BackQuote;

    [System.Serializable]
    public enum ControllerType { Right, Left};
    public ControllerType DefaultController = ControllerType.Right;

    private SimmulatorManager Manager = null;

    private XRController m_xrController;
    private ControllerType m_currentSelected;
    private float m_distance = 0;

    // Initialisation
    public void Init(SimmulatorManager _manager)
    {
        Manager = _manager;

        m_currentSelected = DefaultController;
        SetController();
    }

    private void SetController()
    {
        if (m_currentSelected == ControllerType.Right) m_xrController = Manager.RightController;
        if (m_currentSelected == ControllerType.Left) m_xrController = Manager.LeftController;
    }

    // Type Manipulation
    private Type GetNestedType(object obj, string typeName)
    {
        foreach (var type in m_xrController.GetType().GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public))
        {
            if (type.Name == typeName)
            {
            return type;
            }
        }
        return null;
    }

    private Dictionary<string, object> GetEnumValues(Type enumType)
    {
        Debug.Assert(enumType.IsEnum == true);
        Dictionary<string, object> enumValues = new Dictionary<string, object>();
        foreach (object value in Enum.GetValues(enumType))
        {
            enumValues[Enum.GetName(enumType, value)] = value;
        }
        return enumValues;
    }

    private void UpdateXRControllerState(string interaction, KeyCode inputKey)
    {
        // Update interaction state
        bool state = Input.GetKey(inputKey);
        Type interactionTypes = GetNestedType(m_xrController, "InteractionTypes");
        Dictionary<string, object> interactionTypesEnum = GetEnumValues(interactionTypes);
        MethodInfo updateInteractionType = m_xrController.GetType().GetMethod("UpdateInteractionType", BindingFlags.NonPublic | BindingFlags.Instance);
        updateInteractionType.Invoke(m_xrController, new object[] { interactionTypesEnum[interaction], (object)state });
    }

    private bool GetButtonPressed(InputHelpers.Button button)
    {
        switch (button)
        {
            case InputHelpers.Button.Trigger:
            return Input.GetKey(triggerKey);
            case InputHelpers.Button.Grip:
            return Input.GetKey(gripKey);
            default:
            return false;
        }
    }

    private void LateUpdate()
    {
        // Switch to a different controller?
        if (Input.GetKeyDown(switchControllerKey))
        {    
            int[] ControllerTypes = (int[]) System.Enum.GetValues(typeof(ControllerType));
            int n = ControllerTypes.Length;
            int i = (int) m_currentSelected;
            m_currentSelected = (ControllerType) ((i + 1) % n);
            SetController();

            Debug.LogFormat("Switched controller: {0}", m_xrController.name);
        }
    
        // Ensure we are overriding ControllerInput as well, which is our wrapper for direct button press detection
        ControllerInput controllerInput = m_xrController.GetComponent<ControllerInput>();
        if (controllerInput)
        {
            controllerInput.SetButtonPressProvider(GetButtonPressed);
        }
    
        // Controller movement
        float scroll = Input.mouseScrollDelta.y;
        if (Input.GetMouseButton(0) || scroll != 0)
        {
            // Scroll wheel controls depth
            m_distance += scroll * scrollWheelToDistance;
            float depthOffset = Mathf.Clamp(controllerDefaultDistance + m_distance, controllerMinDistance, controllerMaxDistance);

            // Mouse position sets position in XY plane at current depth
            Vector3 screenPos = Input.mousePosition;
            Ray ray = Manager.HMDCamera.ScreenPointToRay(screenPos);
            Vector3 position = ray.origin + ray.direction * depthOffset;
            m_xrController.transform.position = position;
        }

        // Interaction states
        UpdateXRControllerState("select", selectKey);
        UpdateXRControllerState("activate", activateKey);
    }

#endif
}
