using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionBlock : ProgrammingBlock
{
    // Start is called before the first frame update
    public bool Selected = false;
    public Vector3 SocketPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    protected void FixRotation()
    {
        if (this.Selected) transform.rotation = Quaternion.Euler(this.SocketPos);
    }
}
