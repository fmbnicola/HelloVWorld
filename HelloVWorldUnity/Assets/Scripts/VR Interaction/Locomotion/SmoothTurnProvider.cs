using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothTurnProvider : LocomotionProvider
{
    // Turn angle and time to complete turn
    public float turnSegment = 45.0f;
    public float turnTime = 3.0f;

    // Input left and right
    public InputHelpers.Button rightTurnButton = InputHelpers.Button.PrimaryAxis2DRight;
    public InputHelpers.Button leftTurnButton = InputHelpers.Button.PrimaryAxis2DLeft;

    public List<XRController> controllers = new List<XRController>();

    private float targetTurnAmmount = 0.0f;

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
            targetTurnAmmount = CheckForTurn(controller);

            if (targetTurnAmmount != 0.0f)
            {
                TrySmoothTurn();
            }
        }
    }

    private float CheckForTurn(XRController controller)
    {
        if(controller.inputDevice.IsPressed(rightTurnButton, out bool rightPress))
        {
            if (rightPress) return turnSegment;
        }

        if (controller.inputDevice.IsPressed(leftTurnButton, out bool leftPress))
        {
            if (leftPress) return -turnSegment;
        }

        return 0.0f;
    }

    private void TrySmoothTurn()
    {
        StartCoroutine(TurnRoutine(targetTurnAmmount));

        targetTurnAmmount = 0.0f;
    }

    private IEnumerator TurnRoutine(float turnAmount)
    {
        float previousTurnChange = 0.0f;
        float elapsedTime = 0.0f;

        BeginLocomotion();

        while (elapsedTime <= turnTime)
        {
            // How far are we into the lerp?
            float blend = elapsedTime / turnTime;
            float turnChange = Mathf.Lerp(0, turnAmount, blend);

            // Figure out diference and apply it
            float turnDifference = turnChange - previousTurnChange;
            system.xrRig.RotateAroundCameraUsingRigUp(turnDifference);

            // Save the current amount we've moved, and add up the elapsed time
            previousTurnChange = turnChange;
            elapsedTime += Time.fixedDeltaTime;

            yield return null;
        }

        EndLocomotion();
    }
}
