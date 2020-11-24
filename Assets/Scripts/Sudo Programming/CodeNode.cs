using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



public class CodeNode
{
    public CodeNode ContextNode { get; protected set; }
    public CodeNode Previous { get; protected set; }

    public CodeNode Next;

    public ProgrammingBlock Block { get; private set; }

    public bool Complete = false;



    public CodeNode(CodeNode context, CodeNode prev, ProgrammingBlock block)
    {
        this.ContextNode = context;

        this.Previous = prev;

        this.Block = block;
    }


    public virtual bool Execute(ActionController robotActuator)
    {
        robotActuator.Execute(this);

        return this.Complete;
    }


    public virtual CodeNode GetNext(ActionController robotActuator)
    {
        return this.Next;
    }


    public virtual CodeNode AfterBreak()
    {
        Debug.LogError("Isto nao devia de ter acontecido");
        return null;
    }


    public virtual void Highlight()
    {
        if (this.Block != null)
        {
            this.Block.Highlight();
        }
    }
}
