using UnityEngine;
using UnityEngine.Events;

public class FixedButton : MonoBehaviour
{
    public bool drawGizmos = false;
    public float pressLength;
    public bool pressed;

    // Event to be fired only once when clicked
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }
    public ButtonEvent clickEvent;

    // Clicker
    private Transform clicker;

    // Math Properties
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 normalVector;
    private float distanceTraveled;



    void Start()
    {
        clicker = transform.Find("Clicker");

        //Set Button Properties
        startPosition = clicker.position;
        currentPosition = startPosition;
        normalVector = clicker.up;
        distanceTraveled = 0;
    }

    public Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 direction, Vector3 point, float clampValue)
    {
        //Get heading and maximum
        Vector3 heading = direction.normalized;
        float magnitudeMax = clampValue;

        //Do projection from the point but clamp it
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

    void LateUpdate()
    {
        // Get closes position
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