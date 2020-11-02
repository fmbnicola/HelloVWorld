﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHider : MonoBehaviour
{
    public GameObject handObject = null;

    private HandPhysics handPhysics = null;
    private XRDirectInteractor interactor = null;
    private XRController controller = null;

    private Collider hand_collider = null;
    private Renderer hand_renderer = null;

    //flags
    private bool disconnected = true;
    private bool forceValid = false;
    public bool useHands = true;

    private void Awake()
    {
        handPhysics = handObject.GetComponent<HandPhysics>();
        interactor = GetComponent<XRDirectInteractor>();
        controller = GetComponent<XRController>();

        hand_collider = handObject.GetComponent<Collider>();
        hand_renderer = handObject.GetComponentInChildren<Renderer>();
    }

    private void OnEnable()
    {
        interactor.onSelectEnter.AddListener(InteractorHide);
        interactor.onSelectExit.AddListener(InteractorShow);
    }

    private void Start()
    {
        controller.hideControllerModel = true;
    }

    private void OnDisable()
    {
        interactor.onSelectEnter.RemoveListener(InteractorHide);
        interactor.onSelectExit.RemoveListener(InteractorShow);
    }

    private void Update()
    {
        var device = controller.inputDevice;
        if (disconnected && (device.isValid || forceValid))
        {
            disconnected = false;
            this.Show();
        }
        else if (!disconnected && !(device.isValid || forceValid))
        {
            disconnected = true;
            this.Hide();
        }

        //Turn on and off Indicators
        controller.hideControllerModel = useHands;
    }

    private void InteractorShow(XRBaseInteractable interactable)
    {
        this.Show();
    }

    private void InteractorHide(XRBaseInteractable interactable)
    {
        this.Hide();
    }

    private void Show()
    {
        if (!disconnected && useHands)
        {
            handPhysics.TeleportToTarget();

            hand_collider.enabled = true;
            hand_renderer.enabled = true;
        }
    }

    private void Hide()
    {
        hand_collider.enabled = false;
        hand_renderer.enabled = false;
    }

    public void setForceValid(bool _forceValid)
    {
        forceValid = _forceValid;
    }

    public void setUseHands(bool _useHands)
    {
        useHands = _useHands;

        // Fake input device
        setForceValid(useHands);

        // Show or hide
        if (useHands) Show();
        else Hide();
    }
}
