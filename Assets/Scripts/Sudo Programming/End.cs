using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



public class End : CodeNode
{
    public End(CodeNode context, CodeNode prev) : base(context, prev) { }



    public override bool Execute(ActionController robotActuator)
    {
        robotActuator.Execute(this);

        return this.Complete;
    }

}
