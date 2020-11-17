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
    private LayerMask Mask;
    
    public Transform AnchorPoint;

    private ConnectorCable Cable;

    private bool Active = true;

    public enum States
    {
        OnAnchor,
        Grabbed,
        OnSocket
    }

    public States State { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterPlug(this);

        this.Cable = this.transform.Find("ConnectorCable").GetComponent<ConnectorCable>();

        this.Mask = this.GetComponent<OffsetGrab>().interactionLayerMask;

        this.OnSocket = false;

        this.State = States.OnAnchor;
    }


    // Update is called once per frame
    void Update()
    {
        if(this.Block.Active != this.Active)
        {
            if(this.Block.Active)
            {
                this.Interactable = this.gameObject.AddComponent<OffsetGrab>();

                this.Interactable.interactionLayerMask = this.Mask;
            }
            else
            {
                Destroy(this.GetComponent<OffsetGrab>());

                if (this.State == States.OnSocket)
                {
                    this.Eject();
                }
                else if (this.State == States.Grabbed)
                {
                    this.State = States.OnAnchor;
                }

                this.transform.position = this.AnchorPoint.position;
                this.transform.rotation = this.AnchorPoint.rotation;
            }

            this.Active = this.Block.Active;
        }

        if (this.Active)
        {
            switch (this.State)
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
                    if (!this.OnSocket)
                    {
                        if (this.Interactable.isSelected) this.State = States.Grabbed;
                        else
                        {
                            this.State = States.OnAnchor;

                            this.Cable.Clear();
                        }
                    }
                    break;
            }
        }
        else
        {
            this.transform.position = this.AnchorPoint.position;
            this.transform.rotation = this.AnchorPoint.rotation;
        }
    }    


    public Socket GetConnectedTo()
    {
        return this.ConnectedTo;
    }

    public ProgrammingBlock GetBlock()
    {
        return this.Block;
    }


    public void ConnectTo(Socket socket)
    {
        if (this.OnSocket == true || socket == null) return;

        this.OnSocket = true;
        this.ConnectedTo = socket;
    }

    public void Disconnect()
    {
        this.OnSocket = false;
        this.ConnectedTo = null;
    }

    public void Eject()
    {
        if (this.OnSocket)
        {
            this.Disconnect();

            this.State = States.OnAnchor;

            this.transform.position = this.AnchorPoint.position;
            this.transform.rotation = this.AnchorPoint.rotation;

            this.Cable.Clear();
        }
    }
}
