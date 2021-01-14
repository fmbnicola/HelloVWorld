using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

using Block;

public class Dais : MonoBehaviour
{
    private Computer Computer;

    public Computer.States State { get; private set; }

    private float ReleaseTime;

    [SerializeField]
    private List<Rigidbody> Bodies;

    public float factor = 6;


    // Start is called before the first frame update
    void Start()
    {
        this.Computer = this.transform.parent.GetComponent<Computer>();

        this.Bodies = new List<Rigidbody>();

        this.ReleaseTime = -1;
    }

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


    public void TurnOn()
    {
        this.ReleaseTime = -1;

        foreach (var body in this.Bodies)
        {
            var block = body.GetComponent<ProgrammingBlock>();

            if (block != null) block.Activate();
        }
    }

    public void TurnOff()
    {
        foreach (var body in this.Bodies)
        {
            body.useGravity = true;

            var block = body.GetComponent<ProgrammingBlock>();

            if (block != null) block.Deactivate();
        }

        this.State = Computer.States.Idle;
        this.ReleaseTime = Time.time;
    }


    public List<Rigidbody> GetBodies()
    {
        return this.Bodies;
    }


    #region Holo-Field
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.attachedRigidbody      == null || 
            collider.attachedRigidbody.mass <= 0.1f ||
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

        var block = body.GetComponent<ProgrammingBlock>();
        if (block != null) block.Deactivate();

        this.BodyExited(body);
    }


    private void BodyEntered(Rigidbody body)
    {
        if (this.Bodies.Contains(body)) return;

        this.Bodies.Add(body);

        var block = body.GetComponent<ProgrammingBlock>();

        if (block != null) this.BlockEntered(block);
    }

    private void BlockEntered(ProgrammingBlock block)
    {
        block.Activate();

        if (block.name.StartsWith("StartBlock"))
            this.Computer.StartBlock = block;
    }


    private void BodyExited(Rigidbody body)
    {
        if (!this.Bodies.Contains(body)) return;

        this.Bodies.Remove(body);

        body.useGravity = true;

        var block = body.GetComponent<ProgrammingBlock>();

        if (block != null) this.BlockExited(block);
    }

    private void BlockExited(ProgrammingBlock block)
    {
        block.Deactivate();

        if (block.name.StartsWith("StartBlock"))
            this.Computer.StartBlock = null;
    }
    #endregion
}
