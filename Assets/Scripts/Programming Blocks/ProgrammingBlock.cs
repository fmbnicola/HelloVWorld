using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammingBlock : MonoBehaviour
{
    protected List<Port> Ports;

    [SerializeField]
    float id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool RegisterPort(Port port)
    {
        if (this.Ports == null) this.Ports = new List<Port>();

        if (port.transform.IsChildOf(this.transform) && !this.Ports.Contains(port))
        {
            this.Ports.Add(port);
            return true;
        }

        return false;
    }

    public ProgrammingBlock GetNext()
    {
        foreach(var port in this.Ports)
        {
            if(port.GetPortType() == Port.Types.Out)
            {
                var connectedTo = port.GetConnectedTo();

                if(connectedTo != null) {
                    return connectedTo.GetBlock();
                }
            }
        }

        return null;
    }

    public virtual CodeNode Parse(CodeNode context, CodeNode next)
    {
        return null;
    }
}
