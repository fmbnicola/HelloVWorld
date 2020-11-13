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
        //Debug.Log(this.gameObject.name);
        //Debug.Log(this.gameObject.tag);
        Debug.Log(interactable.gameObject.name);
        if (this.gameObject.CompareTag(interactable.gameObject.tag))
        {
            var colPos = this.collider.bounds.center;
            var colRot = this.collider.transform.rotation;
            Debug.Log(colRot);
            base.OnSelectEnter(interactable);
            if (this.SnapInteractorPosition) interactable.transform.position = colPos;

            if (this.SnapInteractorRotation)
            {
                Debug.Log("hello");
                Debug.Log(interactable.gameObject.name);
                if (interactable.gameObject.GetType() == typeof(FloppyDisk))
                {
                    interactable.GetComponent<FloppyDisk>().Selected = true;
                    interactable.GetComponent<FloppyDisk>().SocketPos = this.transform.rotation.eulerAngles;
                }

                else if (interactable.gameObject.GetType() == typeof(ConditionBlock))
                {
                    interactable.GetComponent<ConditionBlock>().Selected = true;
                    interactable.GetComponent<ConditionBlock>().SocketPos = this.transform.rotation.eulerAngles;
                }


               /* interactable.transform.rotation = this.transform.rotation;
                interactable.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                interactable.gameObject.GetComponent<Rigidbody>().freezeRotation = true;*/
            }

            interactable.GetComponent<Collider>().isTrigger = true;
            Debug.Log(interactable.transform.rotation);
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
        Debug.Log("hello2");
        Debug.Log(interactable.gameObject.GetType());

        if (interactable.gameObject.GetType() == typeof(FloppyDisk))
        {
            interactable.GetComponent<FloppyDisk>().Selected = false;
            interactable.GetComponent<FloppyDisk>().SocketPos = Vec;
        }

       
        else if (interactable.gameObject.GetType() == typeof(ConditionBlock))
        {
            Debug.Log("hello");
            interactable.GetComponent<ConditionBlock>().Selected = false;
            interactable.GetComponent<ConditionBlock>().SocketPos = Vec;
        }

        base.OnSelectExit(interactable);
    }
}
