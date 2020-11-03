using UnityEngine;
using UnityEngine.Events;

public class DynamicButton : MonoBehaviour
{
    public bool drawGizmos = false;
    public float pressLength;
    public bool pressed = false;

    // SpeedLock (so button isn't clicked when parent is being moved)
    public bool speedLock = true;
    public float speedlockThreshold = 0.5f;
    public float speedLockStrength  = 10.0f;

    // Event to be fired only once when clicked
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }
    public ButtonEvent clickEvent;

    //Parent
    private Transform parent;
    private Rigidbody parentRB;

    // Clicker
    private Transform clicker;
    private Rigidbody clickerRB;

    // Math Properties
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 normalVector;
    private float distanceTraveled;


    void Start()
    {
        // Get parts
        parent = transform.parent;
        parentRB = parent.GetComponent<Rigidbody>();

        clicker = transform.Find("Clicker");
        clickerRB = clicker.GetComponent<Rigidbody>();

        // Set Button Properties
        startPosition = clicker.position;
        currentPosition = startPosition;
        normalVector = clicker.up;
        distanceTraveled = 0;
    }

    public Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 direction, Vector3 point, float clampValue)
    {
        // Get heading and maximum
        Vector3 heading = direction.normalized;
        float magnitudeMax = clampValue;

        // Do projection from the point but clamp it
        Vector3 lhs = point - origin;
        float dotP = Vector3.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return origin + heading * dotP;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos && clicker != null)
        {
            Gizmos.DrawLine(startPosition, startPosition - normalVector * pressLength);
            Gizmos.DrawSphere(currentPosition, 0.01f);
            UnityEditor.Handles.Label(currentPosition, distanceTraveled.ToString());
        }
    }

    private void FixedUpdate()
    {
        // if parent has speed apply upward force to clicker to lock it in place
        if (speedLock && parentRB != null && clickerRB != null) {

            var speedDelta = Vector3.Magnitude(parentRB.velocity);

            if (speedDelta >= speedlockThreshold)
            {
                Vector3 force = speedLockStrength * normalVector;
                clickerRB.AddForceAtPosition(force, Vector3.zero);
            }
        }
    }

    void LateUpdate()
    {
        // Reset start position and normal vector
        startPosition = transform.position;
        normalVector  = clicker.up;

        //lock rotation
        clicker.rotation = transform.rotation;

        // Get closest position
        currentPosition = FindNearestPointOnLine(startPosition, -normalVector, clicker.position, pressLength + 0.005f);
        distanceTraveled = Vector3.Distance(startPosition, currentPosition);
        
        // Lock position to linear axis
        clicker.position = currentPosition;

        // If our distance is close enough to what we specified as a press, click!
        if (distanceTraveled >= pressLength)
        {
            if (!pressed)
            {
                pressed = true;
                clickEvent.Invoke();
                Debug.Log("Button Clicked!");
            }
        }
        else
        {
            // If we aren't all the way down, reset our press
            pressed = false;
        }
    }
}