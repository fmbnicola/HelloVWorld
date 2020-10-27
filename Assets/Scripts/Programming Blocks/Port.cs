using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [SerializeField]
    protected ProgrammingBlock Block;

    [SerializeField]
    protected Port ConnectedTo;
    
    public enum Types
    {
        Out,
        In
    }

    [SerializeField]
    private Types Type = Types.Out;


    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterPort(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.Type == Types.Out && this.ConnectedTo != null)
        {
            Debug.DrawLine(this.transform.position, this.ConnectedTo.transform.position);
        }
    }

    public Types GetPortType()
    {
        return this.Type;
    }

    public Port GetConnectedTo()
    {
        return this.ConnectedTo;
    }

    public ProgrammingBlock GetBlock()
    {
        return this.Block;
    }
}
