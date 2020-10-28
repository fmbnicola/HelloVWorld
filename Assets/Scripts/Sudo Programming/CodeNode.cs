using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



public class CodeNode
{
    public CodeNode ContextNode { get; protected set; }
    public CodeNode Previous { get; protected set; }

    public CodeNode Next;

    // added by miguel
    public bool Complete = false;



    public CodeNode(CodeNode context, CodeNode prev)
    {
        this.ContextNode = context;

        this.Previous = prev;
    }


    virtual public bool Execute(ActionController robotActuator)
    {
        // TODO: backend stuff

        robotActuator.Execute(this);

        return this.Complete;
    }
}
