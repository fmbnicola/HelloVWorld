using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



public class CodeNode
{
    public CodeNode ContextNode { get; protected set; }
    public CodeNode Previous { get; protected set; }

    public CodeNode Next;


    public CodeNode(CodeNode context, CodeNode prev)
    {
        this.ContextNode = context;

        this.Previous = prev;
    }


    public void Execute(ActionController actionController)
    {
        //actionController.Execute(this);
    }
}
