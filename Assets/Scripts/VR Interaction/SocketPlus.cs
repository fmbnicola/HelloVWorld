using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketPlus : XRSocketInteractor
{
    public bool SnapInteractorPosition = false;

    public bool SnapInteractorRotation = false;


    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {
        if (this.SnapInteractorPosition) interactable.transform.position = this.transform.position;

        if (this.SnapInteractorRotation) interactable.transform.rotation = this.transform.rotation;

        base.OnSelectEnter(interactable);
    }
}
