using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading;

public class Plug : MonoBehaviour
{
    [SerializeField]
    protected ProgrammingBlock Block;

    [SerializeField]
    private Socket ConnectedTo;

    public bool OnSocket;

    private XRGrabInteractable Interactable;
    
    public Transform AnchorPoint;

    private ConnectorCable Cable;

    private enum States
    {
        OnAnchor,
        Grabbed,
        OnSocket
    }

    private States State = States.OnAnchor;


    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterPlug(this);
        this.Interactable = transform.GetComponent<XRGrabInteractable>();
        this.Cable = this.transform.Find("ConnectorCable").GetComponent<ConnectorCable>();
        this.OnSocket = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.State)
        {
            default:
                break;

            case States.OnAnchor:
                if (this.Interactable.isSelected) this.State = States.Grabbed;
                if (this.OnSocket) this.State = States.OnSocket;

                this.transform.position = this.AnchorPoint.position;
                this.transform.rotation = this.AnchorPoint.rotation;
                break;

            case States.Grabbed:
                if (!this.Interactable.isSelected)
                {
                    this.State = States.OnAnchor;

                    this.transform.position = this.AnchorPoint.position;
                    this.transform.rotation = this.AnchorPoint.rotation;

                    this.Cable.Clear();
                }

                if (this.OnSocket)
                {
                    this.State = States.OnSocket;
                }
                break;

            case States.OnSocket:
                if (this.Interactable.isSelected && !this.OnSocket) this.State = States.Grabbed;
                break;
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
