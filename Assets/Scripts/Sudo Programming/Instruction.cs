using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool Complete = false;

    Instruction(ID id, CodeNode context, CodeNode prev, CodeNode next) : base(context, prev, next)
    {
        this.Id = id;
    }

    public bool Execute(Transform robot) // Replace with the MonoBehaviour class
    {
        //this.Complete = robot.Do(this.Id);

        return this.Complete;
    }
}
