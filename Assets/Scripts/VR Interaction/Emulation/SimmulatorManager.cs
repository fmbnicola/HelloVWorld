using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimmulatorManager : MonoBehaviour
{
#if UNITY_EDITOR
    // Rig
    public XRRig Rig = null;

    // HMD + CharacterController + Controllers
    public Camera HMDCamera                     = null;
    public CharacterController CharController   = null;
    public XRController RightController         = null;
    public XRController LeftController          = null;

    // Simmulators
    private HeadsetSimmulator HMDSimmulator         = null;
    private ControllerSimulator ControllerSimulator = null;


    private void Awake()
    {
        if(Rig == null) Rig = FindObjectOfType<XRRig>();

        HMDCamera = Rig.GetComponentInChildren<Camera>();
        CharController = Rig.GetComponentInChildren<CharacterController>();


        XRController[] controllers = Rig.GetComponentsInChildren<XRController>();
        foreach(XRController controller in controllers)
        {
            if (controller.name == "RightHand Controller") RightController = controller;
            if (controller.name == "LeftHand Controller") LeftController = controller;
        }

        HMDSimmulator = GetComponent<HeadsetSimmulator>();
        if (HMDSimmulator != null) HMDSimmulator.Init(this);

        ControllerSimulator = GetComponent<ControllerSimulator>();
        if (ControllerSimulator != null) ControllerSimulator.Init(this);
    }
#endif
}
