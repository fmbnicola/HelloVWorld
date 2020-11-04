using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Dais : MonoBehaviour
{
    private Computer Computer;

    public Computer.States State { get; private set; }

    private float ReleaseTime;

    [SerializeField]
    private List<Rigidbody> Bodies;

    // Start is called before the first frame update
    void Start()
    {
        this.Computer = this.transform.parent.GetComponent<Computer>();

        this.Bodies = new List<Rigidbody>();

        this.ReleaseTime = -1;
    }

    public float factor = 6;

    // Update is called once per frame
    void Update()
    {
        this.State = this.Computer.State;

        if ( (Time.time - this.ReleaseTime) >= 1 && this.ReleaseTime > 0 ) this.TurnOn();
    }

    private void FixedUpdate()
    {
        var rate = 1 - factor * (Time.fixedDeltaTime / Time.timeScale);

        foreach (var body in this.Bodies)
        {
            var vel = body.velocity;
            var ang = body.angularVelocity;

            switch (this.State)
            {
                default:
                    break;

                case Computer.States.StartUp:
                    body.AddForce(new Vector3(0, 15f, 0));

                    if (vel.magnitude > 0)
                        body.velocity *= rate;

                    if (ang.magnitude > 0)
                        body.angularVelocity *= rate;
                    break;

                case Computer.States.Active:
                    body.useGravity = false;

                    if (vel.magnitude > 0)
                        body.velocity *= rate;

                    if (ang.magnitude > 0)
                        body.angularVelocity *= rate;
                    break;
            }
        }
    }


    public void Release()
    {
        foreach(var body in this.Bodies)
        {
            body.useGravity = true;


            var socket = body.gameObject.GetComponentInChildren<Socket>();
            if ( socket != null )
            {
                
                var plug = socket.GetConnectedTo();
                if ( plug != null)
                {
                    socket.GetComponent<SocketPlus>().socketActive = false;
                    plug.Eject();
                    socket.SetConnectedTo(null);
                }
               

            }
        }

        this.State = Computer.States.Idle;
        this.ReleaseTime = Time.time;
    }

    public void TurnOn()
    {
        this.ReleaseTime = -1;
        foreach (var body in this.Bodies)
        {
            var socket = body.gameObject.GetComponentInChildren<Socket>();
            if ( socket != null )    socket.GetComponent<SocketPlus>().socketActive = true;
        }
    }


    #region Gravity Field
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.attachedRigidbody      == null || 
            collider.attachedRigidbody.mass >= 10f  || 
            collider.transform.parent       != null) return;

        var body = collider.attachedRigidbody;

        this.BodyEntered(body);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.attachedRigidbody      == null ||
            collider.attachedRigidbody.mass >= 10f  ||
            collider.transform.parent       != null) return;

        if (collider.attachedRigidbody == null || collider.attachedRigidbody.mass >= 10f) return;

        var body = collider.attachedRigidbody;

        this.BodyExited(body);
    }


    private void BodyEntered(Rigidbody body)
    {
        if (this.Bodies.Contains(body)) return;

        this.Bodies.Add(body);

        if (body.name == "StartBlock")
            this.Computer.StartBlock = body.gameObject.GetComponent<ProgrammingBlock>();
    }

    private void BodyExited(Rigidbody body)
    {
        if (!this.Bodies.Contains(body)) return;

        this.Bodies.Remove(body);

        body.useGravity = true;

        if (body.name == "StartBlock")
            this.Computer.StartBlock = null;
    }
    #endregion
}
