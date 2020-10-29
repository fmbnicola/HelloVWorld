using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dais : MonoBehaviour
{
    private Computer Computer;

    public Computer.States State { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        this.Computer = this.transform.parent.GetComponent<Computer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.State = this.Computer.State;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.attachedRigidbody == null || collider.attachedRigidbody.mass >= 10f) return;

        switch(this.State)
        {
            default:
                break;

            case Computer.States.StartUp: //Make them lift off the ground
                if(collider.attachedRigidbody.velocity.y < 0.5)
                    collider.attachedRigidbody.AddForce(new Vector3(0, 4, 0));
                break;

            case Computer.States.Active: // Turn off their gravity
                break;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.attachedRigidbody == null || collider.attachedRigidbody.mass >= 10f) return;

        var vel = collider.attachedRigidbody.velocity;

        switch (this.State)
        {
            default:
                break;

            case Computer.States.StartUp: //Make them lift off the ground
                if (vel.y < 0.5)
                    collider.attachedRigidbody.AddForce(new Vector3(0, 4, 0));

                if (vel.magnitude > 0.5)
                    collider.attachedRigidbody.velocity = new Vector3(vel.x * 0.95f, vel.y * 0.95f, vel.z * 0.95f);
                break;

            case Computer.States.Active:
                if (vel.magnitude > 0.5)
                {
                    collider.attachedRigidbody.velocity = new Vector3(vel.x * 0.95f, vel.y * 0.5f, vel.z * 0.95f);
                }
                    
                collider.attachedRigidbody.useGravity = false;
                break;

            case Computer.States.Idle:
                collider.attachedRigidbody.useGravity  = true;
                break;
        }

        if (collider.gameObject.name == "StartBlock")
        {
            this.Computer.StartBlock = collider.gameObject.GetComponent<ProgrammingBlock>();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.attachedRigidbody == null || collider.attachedRigidbody.mass >= 10f) return;

        if (this.Computer.StartBlock != null && collider.gameObject == this.Computer.StartBlock)
            this.Computer.StartBlock = null;

        // Make sure Gravity are right
         collider.attachedRigidbody.useGravity  = true;
    }
}
