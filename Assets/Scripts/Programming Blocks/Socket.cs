using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Socket : MonoBehaviour
{
    [SerializeField]
    protected ProgrammingBlock Block;

    [SerializeField]
    protected Plug ConnectedTo;
    

    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterSocket(this);
        this.transform.GetComponent<XRSocketInteractor>().onSelectEnter.AddListener((interactable) => AttachPlug(interactable));
        this.transform.GetComponent<XRSocketInteractor>().onSelectExit.AddListener((interactable) => DetachPlug(interactable));
    }

    // Update is called once per frame
    void Update()
    {
        if(this.ConnectedTo != null)
        {
            this.ConnectedTo.transform.position = this.transform.position;
            this.ConnectedTo.transform.rotation = this.transform.rotation;
        }
    }

  
    public Plug GetConnectedTo()
    {
        return this.ConnectedTo;
    }

    public void SetConnectedTo(Plug plug)
    {
        this.ConnectedTo = plug;
    }

    public ProgrammingBlock GetBlock()
    {
        return this.Block;
    }

    public void AttachPlug(XRBaseInteractable interactable)
    {
        var plug = interactable.GetComponent<Plug>();

        if (plug == null) return;

        this.ConnectedTo = plug;

        plug.ConnectTo(this);
    }

    public void DetachPlug(XRBaseInteractable interactable)
    {
        var plug = interactable.GetComponent<Plug>();

        if (plug == null) return;

        this.ConnectedTo = null;
        plug.OnSocket    = false;
    }

    public void ForgetPlug()
    {
        if (this.ConnectedTo != null)
        {
            this.ConnectedTo.OnSocket = false;
            this.ConnectedTo.transform.position = this.ConnectedTo.AnchorPoint.position;
            this.ConnectedTo.transform.rotation = this.ConnectedTo.AnchorPoint.rotation;
        }

    }


}
