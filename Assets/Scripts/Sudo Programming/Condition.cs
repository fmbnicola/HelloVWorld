using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    public Sensor Sensor { get; protected set; }
    public Value Value { get; protected set; }
    public Comparator Comparator { get; protected set; }


    public Condition(Sensor sensor, Value value, Comparator comparator)
    {
        this.Sensor = sensor;
        this.Value = value;
        this.Comparator = comparator;
    }


    public bool Check(Transform robot) // Needs to be changed to the MonoBehaviour class
    {
        var sensed = this.Sensor.GetValue(robot);

        return this.Comparator.Compare(sensed, this.Value);
    }
}
