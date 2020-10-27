using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value
{
    public enum ID
    {
        Box,
        Wall
    }

    public ID Id { get; protected set; }

    public Value(ID id)
    {
        this.Id = id;
    }
}
