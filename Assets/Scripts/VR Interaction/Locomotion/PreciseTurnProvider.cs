using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PreciseTurnProvider : LocomotionProvider
{
    // Turn angle and time to complete turn
    public float turnSpeed = 3.0f;

    // Input left and right
    public InputHelpers.Button rightTurnButton = InputHelpers.Button.PrimaryAxis2DRight;
    public InputHelpers.Button leftTurnButton = InputHelpers.Button.PrimaryAxis2DLeft;

    public List<XRController> controllers = new List<XRController>();


    void FixedUpdate()
    {
        if (CanBeginLocomotion())
        {
            CheckForInput();
        }
    }

    private void CheckForInput()
    {
        foreach(XRController controller in controllers)
        {
            CheckForTurn(controller.inputDevice);

        }
    }

    private void CheckForTurn(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
        {
            StartTurn(position.x);
        }
    }


    private void StartTurn(float turnAmmount)
    {
        system.xrRig.RotateAroundCameraUsingRigUp(turnAmmount * turnSpeed * Time.fixedDeltaTime);
    }

}
