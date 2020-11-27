using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using Block;

public class SocketPlus : XRSocketInteractor
{
    public bool SnapInteractorPosition = false;

    public bool SnapInteractorRotation = false;

    private bool WasTrigger = false;

    private Collider Collider;

    private new void Start()
    {
        base.Start();

        this.Collider = this.GetComponent<Collider>();
    }

    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {
        if (this.gameObject.CompareTag("Plug"))
        {
            var self  = this.gameObject.GetComponent<Socket>();
            var other = interactable.gameObject.GetComponent<Plug>();

            if (self.GetBlock() == other.GetBlock())
            {
                Debug.Log("I cant connect to myself");

                other.Disconnect();
 
                return;     
            }
        }

        var colPos = this.Collider.bounds.center;
        var colRot = this.Collider.transform.rotation;

        base.OnSelectEnter(interactable);

        if (this.SnapInteractorPosition) interactable.transform.position = colPos;

        if (this.SnapInteractorRotation)
        {
            interactable.transform.rotation = colRot;


            if (interactable.gameObject.name.Contains("SensorBlock") ||
                     interactable.gameObject.name.Contains("ValueBlock")  ||
                     interactable.gameObject.name.Contains("Comparator"))
            {
                interactable.GetComponent<ConditionBlock>().Selected = true;
                interactable.GetComponent<ConditionBlock>().SocketPos = this.transform.rotation.eulerAngles;
                interactable.GetComponent<ConditionBlock>().SocketConnected = this;
            }
        }

        var body = interactable.GetComponent<Collider>();
        this.WasTrigger = body.isTrigger;
        body.isTrigger = true;
    }

    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        var Vec = new Vector3(0, 0, 0);
        interactable.GetComponent<Collider>().isTrigger = this.WasTrigger;



        if (interactable.gameObject.name.Contains("SensorBlock") ||
                 interactable.gameObject.name.Contains("ValueBlock")  ||
                 interactable.gameObject.name.Contains("Comparator"))
        {
            interactable.GetComponent<ConditionBlock>().Selected = false;
            interactable.GetComponent<ConditionBlock>().SocketPos = Vec;
            interactable.GetComponent<ConditionBlock>().SocketConnected = null;
        }

        base.OnSelectExit(interactable);
    }
}
