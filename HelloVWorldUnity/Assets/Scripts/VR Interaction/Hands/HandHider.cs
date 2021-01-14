using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHider : MonoBehaviour
{
    // Hands
    public GameObject handObject = null;
    private Collider hand_collider = null;
    private Renderer hand_renderer = null;
    private HandPhysics handPhysics = null;

    // Controller
    private XRDirectInteractor interactor = null;
    private XRController controller = null;
    private GameObject indicator = null;

    // Flags
    private bool disconnected = true;
    private bool forceValid = false;
    public bool useHands = true;

    // Flag Setters
    public void setForceValid(bool _forceValid)
    {
        forceValid = _forceValid;
    }

    public void setUseHands(bool _useHands)
    {
        useHands = _useHands;

        if (useHands)
        {
            HideIndicator();
            ShowHand();
        }
        else
        {
            HideHand();
            ShowIndicator();
        }
    }

    private void Awake()
    {
        handPhysics = handObject.GetComponent<HandPhysics>();
        interactor = GetComponent<XRDirectInteractor>();
        controller = GetComponent<XRController>();

        indicator = transform.Find("ModelTransform").gameObject;

        hand_collider = handObject.GetComponent<Collider>();
        hand_renderer = handObject.GetComponentInChildren<Renderer>();
    }

    private void Start()
    {
        setUseHands(useHands);
    }

    private void OnEnable()
    {
        interactor.onSelectEnter.AddListener(InteractorHide);
        interactor.onSelectExit.AddListener(InteractorShow);
    }

    private void OnDisable()
    {
        interactor.onSelectEnter.RemoveListener(InteractorHide);
        interactor.onSelectExit.RemoveListener(InteractorShow);
    }

    private void Update()
    {
        var device = controller.inputDevice;

        //device is active
        if (disconnected && (device.isValid || forceValid))
        {
            disconnected = false;
            this.Show();
        }
        //device is inactive (controllers lost tracking or are sleeping)
        else if (!disconnected && !(device.isValid || forceValid))
        {
            disconnected = true;
            this.Hide();
        }
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
        if (useHands) ShowHand();
        else ShowIndicator();
    }

    private void Hide()
    {
        if (useHands) HideHand();
        else HideIndicator();
    }

    private void ShowHand()
    {
        handPhysics.TeleportToTarget();
        hand_collider.enabled = true;
        hand_renderer.enabled = true;
    }

    private void HideHand()
    {
        hand_collider.enabled = false;
        hand_renderer.enabled = false;
    }

    private void ShowIndicator()
    {
        indicator.SetActive(true);
    }

    private void HideIndicator()
    {
        indicator.SetActive(false);
    }

}
