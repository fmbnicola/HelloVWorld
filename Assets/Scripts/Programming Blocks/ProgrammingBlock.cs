using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammingBlock : MonoBehaviour
{
    protected List<Plug> Plugs;
    protected Socket Socket;

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


    public bool RegisterSocket(Socket socket)
    {

        if (socket.transform.IsChildOf(this.transform) && this.Socket != null)
        {
            this.Socket = socket;
            return true;
        }
        //adicionar erro caso nao seja null

        return false;
    }


    public bool RegisterPlug(Plug plug)
    {
        if (this.Plugs == null) this.Plugs = new List<Plug>();

        if (plug.transform.IsChildOf(this.transform) && !this.Plugs.Contains(plug))
        {
            this.Plugs.Add(plug);
            return true;
        }

        return false;
    }

    public ProgrammingBlock GetNext()
    {
        foreach(var plug in this.Plugs)
        {
            var connectedTo = plug.GetConnectedTo();

            if(connectedTo != null) {
                return connectedTo.GetBlock();
            }
            
        }

        return null;
    }

    public virtual CodeNode Parse(CodeNode context, CodeNode next)
    {
        return null;
    }
}
