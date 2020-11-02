using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPhysics : MonoBehaviour
{

    public float InterpolationSpeed = 20.0f;
    public Transform target = null;

    private Rigidbody rigidBody = null;
    private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        TeleportToTarget();
    }

    private void Update()
    {
        SetTargetPosition();
        SetTargetRotation();
    }

    private void FixedUpdate()
    {
        MoveToController();
        RotateToController();
    }

    private void SetTargetPosition()
    {
        targetPosition = target.position;
    }

    private void SetTargetRotation()
    {
        targetRotation = target.rotation;
    }

    private void MoveToController()
    {
        float time = InterpolationSpeed * (1 - Mathf.Exp(-20 * Time.deltaTime));
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, time);

        rigidBody.velocity = Vector3.zero;
        rigidBody.MovePosition(newPosition);

    }

    private void RotateToController()
    {
        float time = InterpolationSpeed * (1 - Mathf.Exp(-20 * Time.deltaTime));
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, time);

        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.MoveRotation(newRotation);
    }

    public void TeleportToTarget()
    {
        targetPosition = target.position;
        targetRotation = target.rotation;

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
