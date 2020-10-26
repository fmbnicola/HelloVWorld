using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor
{
    public enum ID
    {
        Camera,
        Microphone
    }

    public ID Id { get; protected set; }

    Sensor(ID id)
    {
        this.Id = id;
    }


    public Value GetValue(Transform robot) // Change to the MonoBehaviour class
    {
        // return robot.Sense(this.Id);

        return null;
    }
}
