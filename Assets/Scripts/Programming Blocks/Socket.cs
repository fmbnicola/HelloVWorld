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
        
    }

  
    public Plug GetConnectedTo()
    {
        return this.ConnectedTo;
    }

    public ProgrammingBlock GetBlock()
    {
        return this.Block;
    }

    public void AttachPlug(XRBaseInteractable interactable)
    {
        this.ConnectedTo = interactable.gameObject.GetComponent<Plug>();
        interactable.gameObject.GetComponent<Plug>().OnSocket = true;
    }

    public void DetachPlug(XRBaseInteractable interactable)
    {
        this.ConnectedTo = null;
        interactable.gameObject.GetComponent<Plug>().OnSocket = false;
    }

}
