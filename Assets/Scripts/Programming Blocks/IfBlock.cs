using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IfBlock : ProgrammingBlock
{ 
    private SensorBlock Sensor;
    private ComparatorBlock Comparator;
    private ValueBlock Value;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterSensor(XRBaseInteractable interactable)
    {
        this.Sensor = interactable.GetComponent<SensorBlock>();
    }

    public void DeregisterSensor(XRBaseInteractable interactable)
    {
        var interactableSensor = interactable.GetComponent<SensorBlock>();

        if (interactableSensor.Equals(this.Sensor)) this.Sensor = null;

    }

    public void RegisterComparator(XRBaseInteractable interactable)
    {
        this.Comparator = interactable.GetComponent<ComparatorBlock>();
    }

    public void DeregisterComparator(XRBaseInteractable interactable)
    {
        var interactableComp = interactable.GetComponent<ComparatorBlock>();

        if (interactableComp.Equals(this.Comparator)) this.Comparator = null;
    }

    public void RegisterValue(XRBaseInteractable interactable)
    {
        this.Value = interactable.GetComponent<ValueBlock>();
    }

    public void DeregisterValue(XRBaseInteractable interactable)
    {
        var interactableVal = interactable.GetComponent<ValueBlock>();

        if (interactableVal.Equals(this.Value)) this.Value = null;

    }


    public override CodeNode Parse(CodeNode context, CodeNode prev)
    {
        
        var cond = new Condition(this.Sensor.Parse(), this.Value.Parse(), this.Comparator.Parse());
        return new If(context, prev, cond);
    }
}
