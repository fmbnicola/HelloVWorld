using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public bool OnSocket;


    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterPlug(this);
        this.Interactable = transform.GetComponent<XRGrabInteractable>();
        this.Joint = transform.GetComponent<FixedJoint>();
        this.RigidBody = this.Joint.connectedBody; 
        this.AnchorPoint = this.Joint.connectedAnchor;
        this.OnSocket = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (this.Interactable.isSelected)
        {
            this.DisconnectJoint();
        }
        else
        {
            if ( this.OnSocket == false) this.ReconnectJoint();

            else
            {
               Debug.Log("else");
            }

        }


    }


    private void DisconnectJoint()
    {
        Destroy(this.Joint);
    }

    private void ReconnectJoint()
    {
        if (this.Joint != null) return;

        this.transform.position = this.Block.transform.position + this.AnchorPoint;
        this.transform.eulerAngles = this.Block.transform.eulerAngles;

        this.Joint = this.gameObject.AddComponent<FixedJoint>();
        this.Joint.autoConfigureConnectedAnchor = false;
        this.Joint.enableCollision = false;
        this.Joint.connectedBody = this.RigidBody;
        this.Joint.connectedAnchor = this.AnchorPoint;

    }


    public void ConnectTo(Socket socket)
    {
        if (this.OnSocket == true || socket == null) return;

        this.OnSocket    = true;
        this.ConnectedTo = socket;
    }

    public void Disconnect()
    {
        this.OnSocket    = false;
        this.ConnectedTo = null;
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
