using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public float DefaultLength = 5.0f;
    public GameObject Dot;
    public VRInputModule InputModule;

    private LineRenderer LineRend = null;

    private GameObject Hit;


    private void Awake()
    {
        LineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
        UpdateTarget();
    }

    private void UpdateLine()
    {
        float targetLength = DefaultLength;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPos = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
        {
            endPos = hit.point;
            this.Hit = hit.collider.gameObject;

        }

        Dot.transform.position = endPos;

        LineRend.SetPosition(0, transform.position);
        LineRend.SetPosition(1, endPos);


    }

     private void UpdateTarget()
    {
        if( this.InputModule != null )
        {
            this.InputModule.Target = this.Hit;
        }

    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, DefaultLength);
        return hit;
    }
}
