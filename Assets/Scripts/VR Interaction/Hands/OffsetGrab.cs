using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrab : XRGrabInteractable
{
    private Vector3 interactorPosition = Vector3.zero;
    private Quaternion interactorRotation = Quaternion.identity;

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        StoreInteractor(interactor);
        MatchAttachmentPoints(interactor);
    }

    private void StoreInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = interactor.attachTransform.localPosition;
        interactorRotation = interactor.attachTransform.localRotation;
    }

    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
        ResetAttachmentPoint(interactor);
        ClearInteractor(interactor);
    }

    private void ResetAttachmentPoint(XRBaseInteractor interactor)
    {
        interactor.attachTransform.localPosition = interactorPosition;
        interactor.attachTransform.localRotation = interactorRotation;
    }

    private void ClearInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = Vector3.zero;
        interactorRotation = Quaternion.identity;
    }

    public new float GetDistanceSqrToInteractor(XRBaseInteractor interactor)
    {
        if (!interactor)
            return float.MaxValue;

        float minDistanceSqr = float.MaxValue;

        var delColliders = new List<int>();
        var ind = 0;

        foreach (var col in m_Colliders)
        {
            if (col == null)
            {
                delColliders.Add(ind);
                continue;
            }

            var offset = (interactor.attachTransform.position - col.transform.position);
            minDistanceSqr = Mathf.Min(offset.sqrMagnitude, minDistanceSqr);

            ind++;
        }

        foreach (var i in delColliders) m_Colliders.RemoveAt(i);

        return minDistanceSqr;
    }
}
