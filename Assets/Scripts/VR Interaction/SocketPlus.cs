using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketPlus : XRSocketInteractor
{
    public bool SnapInteractorPosition = false;

    public bool SnapInteractorRotation = false;

    private Collider collider;
    private void Start()
    {
        this.collider = this.GetComponent<Collider>();
    }

    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {

        if (this.gameObject.CompareTag(interactable.gameObject.tag))
        {

            if (this.gameObject.CompareTag("Plug"))
            {
               var Myself = this.gameObject.GetComponent<Socket>();
               var Plug = interactable.gameObject.GetComponent<Plug>();
               if (  Myself.GetBlock() == Plug.GetBlock())
               {
                    Debug.Log("I cant connect to myself");

                    Plug.NotMySelf();
 
                    return;     
               }
            }




            var colPos = this.collider.bounds.center;
            var colRot = this.collider.transform.rotation;

            base.OnSelectEnter(interactable);

            if (this.SnapInteractorPosition) interactable.transform.position = colPos;

            if (this.SnapInteractorRotation)
            {
                interactable.transform.rotation = colRot;

                if (interactable.gameObject.name.Contains("FloppyDisk"))
                {
                    interactable.GetComponent<FloppyDisk>().Selected = true;
                    interactable.GetComponent<FloppyDisk>().SocketPos = this.transform.rotation.eulerAngles;
                    interactable.GetComponent<FloppyDisk>().SocketConnected = this;
                }

                else if (interactable.gameObject.name.Contains("SensorBlock") ||
                         interactable.gameObject.name.Contains("ValueBlock"))
                {
                    interactable.GetComponent<ConditionBlock>().Selected = true;
                    interactable.GetComponent<ConditionBlock>().SocketPos = this.transform.rotation.eulerAngles;
                    interactable.GetComponent<ConditionBlock>().SocketConnected = this;
                }

            }

            interactable.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            var Vec = new Vector3(0, 0.1f, 0);
            Debug.Log("tipos errados");
            interactable.transform.position = interactable.transform.position + Vec;
            interactable.transform.rotation = interactable.transform.rotation;
        }

        
    }
    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        var Vec = new Vector3(0, 0, 0);
        interactable.GetComponent<Collider>().isTrigger = false;

        if (interactable.gameObject.name.Contains("FloppyDisk"))
        {
            interactable.GetComponent<FloppyDisk>().Selected = false;
            interactable.GetComponent<FloppyDisk>().SocketPos = Vec;
            interactable.GetComponent<FloppyDisk>().SocketConnected = null;
        }


        else if (interactable.gameObject.name.Contains("SensorBlock") ||
                 interactable.gameObject.name.Contains("ValueBlock"))
        {
            interactable.GetComponent<ConditionBlock>().Selected = false;
            interactable.GetComponent<ConditionBlock>().SocketPos = Vec;
            interactable.GetComponent<ConditionBlock>().SocketConnected = null;
        }

        base.OnSelectExit(interactable);
    }
}
