using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HeadsetSimmulator : MonoBehaviour
{
#if UNITY_EDITOR

    //Player Stats
    public float Speed = 3.0f;
    public float Height = 1.70f;

    //Movement Keybindings
    public KeyCode FowardKey    = KeyCode.W;
    public KeyCode LeftKey      = KeyCode.A;
    public KeyCode BackwardKey  = KeyCode.S;
    public KeyCode RightKey     = KeyCode.D;

    // Camera View State
    private class CameraState
    {
        public float yaw;
        public float pitch;
        public float roll;

        public void SetFromTransform(Transform t)
        {
            pitch = t.eulerAngles.x;
            yaw = t.eulerAngles.y;
            roll = t.eulerAngles.z;
        }

        public void LerpTowards(CameraState target, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
            pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
            roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);
        }

        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(pitch, yaw, roll);
        }
    }

    CameraState CurrentCameraState = new CameraState();
    CameraState TargetCameraState = new CameraState();

    // Camera View Stats
    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
    public float rotationLerpTime = 0.01f;

    private SimmulatorManager Manager = null;

    // Initialisation
    public void Init(SimmulatorManager _manager)
    {
        Manager = _manager;

        //Init Camera View
        CurrentCameraState.SetFromTransform(Manager.HMDCamera.transform);
        TargetCameraState.SetFromTransform(Manager.HMDCamera.transform);
    }

    private void SetHeight() {

        Vector3 localpos = Manager.HMDCamera.transform.localPosition;
        localpos.y = Height;
        Manager.HMDCamera.transform.localPosition = localpos;
    }

    private void LateUpdate()
    {
        // Look
        CheckForViewChange();
    }

    private void FixedUpdate()
    {
        // Set Height
        SetHeight();

        // Move
        CheckForMovement();
    }

    private void CheckForMovement()
    {
        int foward = Input.GetKey(FowardKey) ? 1 : 0;
        int left = Input.GetKey(LeftKey) ? 1 : 0;
        int backward = Input.GetKey(BackwardKey) ? 1 : 0;
        int right = Input.GetKey(RightKey) ? 1 : 0;

        float x_axis = right - left;
        float y_axis = foward - backward;

        Vector2 position = new Vector2(x_axis, y_axis).normalized;

        HandleMovement(position);
    }

    private void HandleMovement(Vector2 position)
    {
        // Apply the touch postion to the head's forward Vector
        Vector3 direction = new Vector3(position.x, 0, position.y);
        Vector3 headRotation = new Vector3(0, Manager.HMDCamera.transform.eulerAngles.y, 0);

        // Rotate the input direction by the horizontal head rotation
        direction = Quaternion.Euler(headRotation) * direction;

        // Apply speed and move
        Vector3 movement = direction * Speed;
        Manager.CharController.Move(movement * Time.fixedDeltaTime);
    }

    private void CheckForViewChange()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * -1);
            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
            TargetCameraState.pitch += mouseMovement.y * mouseSensitivityFactor;

            HandleViewChange();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void HandleViewChange()
    {
        var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.fixedDeltaTime);
        CurrentCameraState.LerpTowards(TargetCameraState, rotationLerpPct);

        CurrentCameraState.UpdateTransform(Manager.HMDCamera.transform);
    }
#endif
}
