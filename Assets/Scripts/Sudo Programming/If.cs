using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



public class If : CodeNode
{
    public Condition Condition { get; protected set; }
    public CodeNode NextIfTrue;


    public If(CodeNode context, CodeNode prev, Condition cond) : base(context, prev)
    {
        this.Condition  = cond;
    }


    public override CodeNode GetNext(ActionController robot)
    {
        if (this.NextIfTrue != null && this.Condition.Check(robot)) return this.NextIfTrue;

        return this.Next;
    }


    public override CodeNode AfterBreak()
    {
        return this.Next;
    }
}
