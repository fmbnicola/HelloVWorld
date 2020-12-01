using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserSensor : MonoBehaviour
{
    public GameObject LightQuad;
    public XRRayInteractor interactor;

    private bool detecting = false;

    #region Detect Events
    [System.Serializable]
    public class DetectEvent : UnityEvent { }

    public DetectEvent OnDetect;
    public DetectEvent OnDetectEnd;
    #endregion

    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = transform.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        interactor.GetCurrentRaycastHit(out RaycastHit hit);
        if(interactor.gameObject.activeSelf && hit.collider == collider)
        {
            if (detecting == false) OnDetect.Invoke();
            detecting = true;
        }
        else
        {
            if (detecting == true) OnDetectEnd.Invoke();
            detecting = false;
        }

        // update light transform
        LightQuad.SetActive(detecting);
        if(detecting) LightQuad.transform.LookAt(Camera.main.transform);
    }
}
