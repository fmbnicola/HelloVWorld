using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class ConnectorCable : MonoBehaviour
{
    public Transform AnchorPoint;

    public Transform Plug;

    private Spline Spline;
    private RopeBuilder Builder;

    // Start is called before the first frame update
    void Start()
    {
        this.Spline  = this.GetComponent<Spline>();
        this.Builder = this.GetComponent<RopeBuilder>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Spline.nodes[0].Position = this.AnchorPoint.position;
        this.Spline.nodes[this.Spline.nodes.Count - 1].Position = this.Plug.position;

        //this.Builder.segmentCount = (int) Mathf.Round((this.Plug.position - this.AnchorPoint.position).magnitude * 5.2f);
    }
}
