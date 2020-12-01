using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    public float speed = 5.0f;
    public XRController controller = null;

    private Animator animator = null;

    public GameObject PointerRay = null;

    // Finger Groups
    private readonly List<Finger> gripFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    private readonly List<Finger> pointFingers = new List<Finger>()
    {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb),
    };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Store Input
        CheckGrip();
        CheckPointer();
        CheckThumb();

        // Smooth input values 
        SmoothFinger(gripFingers);
        SmoothFinger(pointFingers);

        // Apply smoothed values
        AnimateFinger(gripFingers);
        AnimateFinger(pointFingers);
    }

    private void CheckGrip()
    {
        if(controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            SetFingerTargets(gripFingers, gripValue);

    }

    private void CheckPointer()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float pointerValue))
            SetFingerTargets(pointFingers, pointerValue);
    }

    private void CheckThumb()
    {
        var touching = false;
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primary_touch_test))
            touching |= primary_touch_test;
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondary_touch_test))
            touching |= secondary_touch_test;


        bool gripping = false;
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            
            gripping = (gripValue > 0.5f);

        //Pointer ray
        if (touching && gripping)
        {
            SetFingerTarget(pointFingers[1], 1.0f);

            if (PointerRay != null) PointerRay.SetActive(true);
        }
        else
        {
            if (PointerRay != null && PointerRay.active) PointerRay.SetActive(false);
        }
    }

    private void SetFingerTarget(Finger finger, float value)
    {
        finger.target = value;
    }

    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach(Finger finger in fingers)
        {
            finger.target = value;
        }
    }

    private void SmoothFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger, blend);
    }
}