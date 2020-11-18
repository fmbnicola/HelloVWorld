using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



public class While : Conditional
{
    public While(CodeNode context, CodeNode prev, Condition cond) : base(context, prev, cond) { }


    public override CodeNode AfterBreak()
    {
        var inBlock = this.NextIfTrue;

        inBlock.Complete = false;

        while(inBlock.Next != null)
        {
            inBlock = inBlock.Next;

            inBlock.Complete = false;
        }

        return this;
    }
}
