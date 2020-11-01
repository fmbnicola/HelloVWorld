using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;

public class ControllerSimulator : MonoBehaviour
{
#if UNITY_EDITOR
    public float controllerDefaultDistance  = 1;
    public float controllerMaxDistance      = 2;
    public float controllerMinDistance      = 0;
    public float scrollWheelToDistance      = 0.1f;

    public KeyCode selectKey            = KeyCode.Mouse1;
    public KeyCode activateKey          = KeyCode.Return;
    public KeyCode switchControllerKey  = KeyCode.BackQuote;

    [System.Serializable]
    public enum ControllerType { Right, Left};
    public ControllerType DefaultController = ControllerType.Right;

    private SimmulatorManager Manager = null;

    //Controller encapsulating class
    private class Controller
    {
        public XRController XRController;
        public bool Select = false;

        public Controller(XRController xrcontroller)
        {
            XRController = xrcontroller;
        }
    }

    private List<Controller> Controllers;
    private ControllerType ActiveControllerType;
    private Controller ActiveController;

    private float m_distance = 0;

    // Initialisation
    public void Init(SimmulatorManager _manager)
    {
        Manager = _manager;

        Controllers = new List<Controller>();
        Controllers.Add(new Controller(Manager.RightController));
        Controllers.Add( new Controller(Manager.LeftController));

        ActiveControllerType = DefaultController;
        SetController();
    }

    private void SetController()
    {
        ActiveController = Controllers[(int) ActiveControllerType];
    }

    // Type Manipulation
    private Type GetNestedType(object obj, string typeName)
    {
        foreach (var type in obj.GetType().GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public))
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

    private void UpdateXRControllerState(XRController controller, string interaction, KeyCode inputKey)
    {
        // Update interaction state
        bool state = Input.GetKey(inputKey);
        Type interactionTypes = GetNestedType(controller, "InteractionTypes");
        Dictionary<string, object> interactionTypesEnum = GetEnumValues(interactionTypes);
        MethodInfo updateInteractionType = controller.GetType().GetMethod("UpdateInteractionType", BindingFlags.NonPublic | BindingFlags.Instance);
        updateInteractionType.Invoke(controller, new object[] { interactionTypesEnum[interaction], (object)state });
    }

    private void HoldXRControllerState(XRController controller, string interaction)
    {
        // Update interaction state
        bool state = true;
        Type interactionTypes = GetNestedType(controller, "InteractionTypes");
        Dictionary<string, object> interactionTypesEnum = GetEnumValues(interactionTypes);
        MethodInfo updateInteractionType = controller.GetType().GetMethod("UpdateInteractionType", BindingFlags.NonPublic | BindingFlags.Instance);
        updateInteractionType.Invoke(controller, new object[] { interactionTypesEnum[interaction], (object)state });
    }

    private bool GetButtonPressed(InputHelpers.Button button)
    {
        switch (button)
        {
            case InputHelpers.Button.Trigger:
                return Input.GetKey(KeyCode.Alpha0); //FIXME

            case InputHelpers.Button.Grip:
                return Input.GetKey(KeyCode.Alpha0); //FIXME

            default:
            return false;
        }
    }

    private void Update()
    {
        // Controller Movement
        float scroll = Input.mouseScrollDelta.y;
        if (Input.GetMouseButton(0) || scroll != 0)
        {
            // Scroll wheel controls distance
            m_distance += scroll * scrollWheelToDistance;
            float depthOffset = Mathf.Clamp(controllerDefaultDistance + m_distance, controllerMinDistance, controllerMaxDistance);

            // Mouse position sets position in XY plane at current depth
            Vector3 screenPos = Input.mousePosition;
            Ray ray = Manager.HMDCamera.ScreenPointToRay(screenPos);
            
            // Position controller
            Vector3 position = ray.origin + ray.direction * depthOffset;
            ActiveController.XRController.transform.position = position;
        }

        // Controller Rotation
        if (Input.GetMouseButton(2))
        {
            var mouseMovement = new Vector2(Input.GetAxis("Mouse X") * -1, Input.GetAxis("Mouse Y"));

            var transform = ActiveController.XRController.transform;

            transform.RotateAround(transform.position, Vector3.up, mouseMovement.x);
            var axis = Vector3.Cross(Vector3.up, Manager.HMDCamera.transform.forward);
            transform.RotateAround(transform.position, axis, mouseMovement.y);
        }
        else
        {
            // Rotate with Camera
            var rot = ActiveController.XRController.transform.eulerAngles;
            rot.y = Manager.HMDCamera.transform.eulerAngles.y;
            ActiveController.XRController.transform.eulerAngles = rot;
        }
    }

    private void LateUpdate()
    {
        // Switch to a different controller?
        if (Input.GetKeyDown(switchControllerKey))
        {
            //save input state
            ActiveController.Select = (bool) Input.GetKey(selectKey);

            int[] ControllerTypes = (int[]) System.Enum.GetValues(typeof(ControllerType));
            int n = ControllerTypes.Length;
            int i = (int) ActiveControllerType;
            ActiveControllerType = (ControllerType) ((i + 1) % n);
            SetController();

            Debug.LogFormat("Switched controller: {0}", ActiveController.XRController.name);
        }

        // Ensure we are overriding ControllerInput as well, which is our wrapper for direct button press detection
        ControllerInput controllerInput = ActiveController.XRController.GetComponent<ControllerInput>();
        if (controllerInput)
        {
            controllerInput.SetButtonPressProvider(GetButtonPressed);
        }

        //Hold input state for inactive controllers
        foreach(Controller controller in Controllers)
        {
            if (controller != ActiveController && controller.Select) {
                HoldXRControllerState(controller.XRController, "select");
            }
        }

        // Interaction states
        UpdateXRControllerState(ActiveController.XRController, "select", selectKey);
        UpdateXRControllerState(ActiveController.XRController, "activate", activateKey);

    }

#endif
}
