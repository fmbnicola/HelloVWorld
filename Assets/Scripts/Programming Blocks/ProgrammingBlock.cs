using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammingBlock : MonoBehaviour
{
    protected List<Plug> Plugs;
    protected Socket Socket;

    protected MaterialPropertyBlock propertyBlock;
    public Renderer BlockRenderer;

    [SerializeField]
    public bool Active;


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

    public virtual ProgrammingBlock GetNext()
    {
        if (this.Plugs == null || this.Plugs.Count == 0) return null;

        var plug = this.Plugs[0];

        if (plug != null)
        {
            var connectedTo = plug.GetConnectedTo();

            if (connectedTo != null)
            {
                return connectedTo.GetBlock();
            }
        }

        return null;
    }


    public virtual CodeNode Parse(CodeNode context, CodeNode prev)
    {
        return null;
    }


    public void Activate()
    {
        if (!this.Active) this.Active = true;
    }


    public void Deactivate()
    {
        if (this.Active) this.Active = false;
    }


    public virtual void Highlight()
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetFloat("_OutlineWidth", 0.1f);
        BlockRenderer.SetPropertyBlock(propertyBlock);

        Debug.Log(this.ToString() + " highlighted");
    }

    public virtual void UnHighlight()
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetFloat("_OutlineWidth", 0.0f);
        BlockRenderer.SetPropertyBlock(propertyBlock);

        Debug.Log(this.ToString() + " remove highlight");
    }
}
