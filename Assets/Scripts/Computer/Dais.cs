using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dais : MonoBehaviour
{
    private Computer Computer;

    public Computer.States State { get; private set; }

    private float AntiGravityForce = 5;


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
                if(collider.attachedRigidbody.velocity.y < 5)
                    collider.attachedRigidbody.AddForce(new Vector3(0,6,0));
                break;

            case Computer.States.Active: // Turn off their gravity
                collider.attachedRigidbody.useGravity  = false;
                collider.attachedRigidbody.isKinematic = true;
                break;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.attachedRigidbody == null || collider.attachedRigidbody.mass >= 10f) return;

        switch (this.State)
        {
            default:
                break;

            case Computer.States.StartUp: //Make them lift off the ground
                if (collider.attachedRigidbody.velocity.y < 5)
                    collider.attachedRigidbody.AddForce(new Vector3(0, 6, 0));
                break;

            case Computer.States.Idle:
                collider.attachedRigidbody.useGravity  = true;
                collider.attachedRigidbody.isKinematic = false;
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

        // Make sure Gravity and Kinematic are right
         collider.attachedRigidbody.useGravity  = true;
         collider.attachedRigidbody.isKinematic = false;
    }
}
