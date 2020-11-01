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

    // Hands
    public HandPhysics rightHand = null;
    public HandPhysics leftHand = null;

    // Simmulators
    private HeadsetSimmulator HMDSimulator         = null;
    private ControllerSimulator ControllerSimulator = null;


    private void Awake()
    {
        if(Rig == null) Rig = FindObjectOfType<XRRig>();

        HMDCamera = Rig.GetComponentInChildren<Camera>();
        CharController = Rig.GetComponentInChildren<CharacterController>();


        XRController[] controllers = Rig.GetComponentsInChildren<XRController>();
        foreach(XRController controller in controllers)
        {
            if (controller.name == "RightHand Controller")
            {
                RightController = controller;
                RightController.GetComponent<HandHider>().setForceValid(true);
            }
            if (controller.name == "LeftHand Controller")
            {
                LeftController = controller;
                LeftController.GetComponent<HandHider>().setForceValid(true);
            }
        }

        //Hands
        var rhand = transform.parent.Find("Hand_Right").gameObject;
        rightHand = rhand.GetComponent<HandPhysics>();

        var lhand = transform.parent.Find("Hand_Left").gameObject;
        leftHand = lhand.GetComponent<HandPhysics>();

        //Simulatros
        HMDSimulator = GetComponent<HeadsetSimmulator>();
        if (HMDSimulator != null) HMDSimulator.Init(this);

        ControllerSimulator = GetComponent<ControllerSimulator>();
        if (ControllerSimulator != null) ControllerSimulator.Init(this);
    }
#endif
}
