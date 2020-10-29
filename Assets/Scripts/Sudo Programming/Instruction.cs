using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



public class Instruction : CodeNode
{
    public enum ID
    {
        Walk,
        Grab,
        Drop,
        Rotate
    }

    public ID Id { get; protected set; }

    //public bool Complete = false;


    public Instruction(ID id, CodeNode context, CodeNode prev) : base(context, prev)
    {
        this.Id = id;
    }


    override public bool Execute(ActionController robotActuator)
    {
        // TODO: backend stuff

        robotActuator.Execute(this);

        return this.Complete;
    }
}
