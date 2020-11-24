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
        TurnRight,
        TurnLeft
    }

    public ID Id { get; protected set; }

    //public bool Complete = false;


    public Instruction(ID id, CodeNode context, CodeNode prev, ProgrammingBlock block) :
        base(context, prev, block)
    {
        this.Id = id;
    }


    override public bool Execute(ActionController robotActuator)
    {
        robotActuator.Execute(this);

        return this.Complete;
    }
}
