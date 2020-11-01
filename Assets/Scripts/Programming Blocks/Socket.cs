using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField]
    protected ProgrammingBlock Block;

    [SerializeField]
    protected Plug ConnectedTo;
    

    // Start is called before the first frame update
    void Start()
    {
        this.Block.RegisterSocket(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    public Plug GetConnectedTo()
    {
        return this.ConnectedTo;
    }

    public ProgrammingBlock GetBlock()
    {
        return this.Block;
    }

}
