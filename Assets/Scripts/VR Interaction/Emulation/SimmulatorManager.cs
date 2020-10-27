using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimmulatorManager : MonoBehaviour
{
    // Rig
    public XRRig Rig = null;

    // HMD + Controllers
    public Camera HMDCamera = null;
    public XRController RightController = null;
    public XRController LeftController = null;

    // Simmulators
    private HeadsetSimmulator HMDSimmulator = null;
    private ControllerSimulator ControllerSimulator = null;


    private void Awake()
    {
        if(Rig == null) Rig = FindObjectOfType<XRRig>();

        HMDCamera = Rig.GetComponentInChildren<Camera>();

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
