using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If : CodeNode
{
    public Condition Condition { get; protected set; }
    public CodeNode NextIfTrue { get; protected set; }

    If(CodeNode context, CodeNode prev, CodeNode next, Condition cond, CodeNode nextTrue) : base(context, prev, next)
    {
        this.Condition  = cond;
        this.NextIfTrue = nextTrue;
    }

    public CodeNode GetNext(Transform robot) // Replace with the MonoBehaviour class
    {
        if (this.Condition.Check(robot)) return this.NextIfTrue;

        return this.Next;
    }
}
