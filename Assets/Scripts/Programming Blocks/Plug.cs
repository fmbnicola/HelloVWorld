using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using SplineMesh;

public class Plug : MonoBehaviour
{
    [SerializeField]
    protected ProgrammingBlock Block;

    private Socket ConnectedTo;
    public bool OnSocket;

    private XRGrabInteractable Interactable;
    
    public Transform AnchorPoint;

    public Spline Cable;
    public Mesh CableMesh;
    public Material CableMaterial;
    private SplineSmoother Smoother;

    private float LastAdded;

    private enum States
    {
        OnAnchor,
        Grabbed,
        OnSocket
    }

    private States State = States.OnAnchor;


    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterPlug(this);
        this.Interactable = transform.GetComponent<XRGrabInteractable>();
        this.OnSocket = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.State)
        {
            default:
                break;

            case States.OnAnchor:
                if (this.Interactable.isSelected) this.State = States.Grabbed;
                if (this.OnSocket) this.State = States.OnSocket;

                this.transform.position = this.AnchorPoint.position;
                this.transform.rotation = this.AnchorPoint.rotation;

                if (this.Cable != null)
                {
                    Destroy(this.Cable.gameObject);
                    this.Cable = null;
                }
                break;

            case States.Grabbed:
                if (!this.Interactable.isSelected) this.State = States.OnAnchor;
                if (this.OnSocket) this.State = States.OnSocket;

                if (this.Cable == null)
                {
                    this.Cable = this.MakeCable();

                    this.LastAdded = Time.time;
                }

                this.ExtendCable();
                break;

            case States.OnSocket:
                if (this.Interactable.isSelected) this.State = States.Grabbed;

                this.ExtendCable();
                break;
        }
    }


    public void ConnectTo(Socket socket)
    {
        if (this.OnSocket == true || socket == null) return;

        this.OnSocket    = true;
        this.ConnectedTo = socket;
    }

    public void Disconnect()
    {
        this.OnSocket    = false;
        this.ConnectedTo = null;
    }

    public Socket GetConnectedTo()
    {
        return this.ConnectedTo;
    }

    public ProgrammingBlock GetBlock()
    {
        return this.Block;
    }


    private Spline MakeCable()
    {
        var gameObject = new GameObject();

        var worldPosition = this.AnchorPoint.TransformPoint(Vector3.zero);

        var spline = gameObject.AddComponent<Spline>();

        spline.nodes.Add(new SplineNode(worldPosition, worldPosition));

        worldPosition += Vector3.down * 0.1f;
        spline.nodes.Add(new SplineNode(worldPosition, worldPosition));

        var meshTiling = gameObject.AddComponent<SplineMeshTiling>();

        #region Mesh
        meshTiling.generateCollider = false;
        meshTiling.updateInPlayMode = true;
        meshTiling.curveSpace = true;

        meshTiling.mode = MeshBender.FillingMode.StretchToInterval;

        meshTiling.mesh     = this.CableMesh;
        meshTiling.material = this.CableMaterial;

        meshTiling.scale = new Vector3(0.05f, 0.05f, 0.05f);
        meshTiling.rotation = new Vector3(0, 90, 0);
        #endregion

        this.Smoother = gameObject.AddComponent<SplineSmoother>();
        this.Smoother.curvature = 0.2f;


        return spline;
    }

    private bool ExtendCable()
    {
        if (Time.time - this.LastAdded > 0.5)
        {
            var lastNode = this.Cable.nodes[this.Cable.nodes.Count - 1];

            var worldPosition = this.transform.TransformPoint(Vector3.zero);

            var vector = lastNode.Position - worldPosition;

            if (vector.magnitude > 0.2)
            {
                this.Cable.AddNode(new SplineNode(worldPosition, worldPosition));

                this.LastAdded = Time.time;

                return true;
            }
        }

        return false;
    }
}
