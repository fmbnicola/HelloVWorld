using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Plug : MonoBehaviour
{
    [SerializeField]
    protected ProgrammingBlock Block;

    [SerializeField]
    protected Socket ConnectedTo;

    private XRGrabInteractable Interactable;
    private FixedJoint Joint;
    private Vector3 AnchorPoint;
    private Rigidbody RigidBody;


    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterPlug(this);
        this.Interactable = transform.GetComponent<XRGrabInteractable>();
        this.Joint = transform.GetComponent<FixedJoint>();
        this.RigidBody = this.Joint.connectedBody; 
        this.AnchorPoint = this.Joint.connectedAnchor;


    }

    // Update is called once per frame
    void Update()
    {
        if (this.Interactable.isSelected)
        {
            this.Disconnect();
        }
        else
        {
            this.Reconnect();
        }


    }

    private void Disconnect()
    {
        Destroy(this.Joint);
    }

    private void Reconnect()
    {
        if (this.Joint != null) return;

        this.transform.position = this.Block.transform.position + this.AnchorPoint;
        this.Joint = this.gameObject.AddComponent<FixedJoint>();
        this.Joint.autoConfigureConnectedAnchor = false;
        this.Joint.enableCollision = false;
        this.Joint.connectedBody = this.RigidBody;
        this.Joint.connectedAnchor = this.AnchorPoint;

    }


    public Socket GetConnectedTo()
    {
        return this.ConnectedTo;
    }

    public ProgrammingBlock GetBlock()
    {
        return this.Block;
    }


}
