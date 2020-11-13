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
        Debug.Log(this.gameObject.name);
        Debug.Log(this.gameObject.tag);
        Debug.Log(interactable.gameObject.tag);
        if (this.gameObject.CompareTag(interactable.gameObject.tag))
        {
            var colPos = this.collider.bounds.center;
            var colRot = this.collider.transform.rotation;
            base.OnSelectEnter(interactable);
            if (this.SnapInteractorPosition) interactable.transform.position = colPos;

            if (this.SnapInteractorRotation)
            {
                interactable.transform.rotation = colRot;
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
        interactable.GetComponent<Collider>().isTrigger = false;

        base.OnSelectExit(interactable);
    }
}
