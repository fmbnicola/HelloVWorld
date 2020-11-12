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
        if (tag.Equals(interactable.gameObject.tag))
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
        else Debug.Log("tipos errados");

        
    }
    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        interactable.GetComponent<Collider>().isTrigger = false;

        base.OnSelectExit(interactable);
    }
}
