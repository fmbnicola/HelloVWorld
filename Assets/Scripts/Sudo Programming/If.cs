using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If : CodeNode
{
    public Condition Condition { get; protected set; }
    public CodeNode NextIfTrue;


    public If(CodeNode context, CodeNode prev, Condition cond) : base(context, prev)
    {
        this.Condition  = cond;
    }


    public CodeNode GetNext(Transform robot) // Replace with the MonoBehaviour class
    {
        if (this.Condition.Check(robot)) return this.NextIfTrue;

        return this.Next;
    }
}
