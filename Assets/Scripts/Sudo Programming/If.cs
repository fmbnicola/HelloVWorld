using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;


public class If : Conditional
{
    public If(CodeNode context, CodeNode prev, Condition cond) : base(context, prev, cond)
    {
        this.Condition  = cond;
    }


    public override CodeNode AfterBreak()
    {
        return this.Next;
    }
}
