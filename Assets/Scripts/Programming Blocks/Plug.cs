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
    
    public Transform AnchorPoint;
    private Rigidbody RigidBody;

    public bool OnSocket;


    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterPlug(this);
        this.Interactable = transform.GetComponent<XRGrabInteractable>();
        this.OnSocket = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.Interactable.isSelected && !this.OnSocket)
        {
            this.transform.position    = this.AnchorPoint.position;
            this.transform.eulerAngles = this.AnchorPoint.eulerAngles;
        }
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
