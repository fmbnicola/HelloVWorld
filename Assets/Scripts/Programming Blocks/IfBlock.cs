﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IfBlock : ProgrammingBlock
{ 
    private SensorBlock Sensor;
    private ComparatorBlock Comparator;
    private ValueBlock Value;

    public SocketPlus SensorSocket, ComparatorSocket, ValueSocket;


    // Start is called before the first frame update
    void Start()
    {
        this.SensorSocket.onSelectEnter.AddListener((interactable) => RegisterSensor(interactable));
        this.SensorSocket.onSelectExit.AddListener((interactable) => DeregisterSensor(interactable));

        this.ComparatorSocket.onSelectEnter.AddListener((interactable) => RegisterComparator(interactable));
        this.ComparatorSocket.onSelectExit.AddListener((interactable) => DeregisterComparator(interactable));

        this.ValueSocket.onSelectEnter.AddListener((interactable) => RegisterValue(interactable));
        this.ValueSocket.onSelectExit.AddListener((interactable) => DeregisterValue(interactable));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterSensor(XRBaseInteractable interactable)
    {
        Debug.Log("RegisterSensor");
       
        this.Sensor = interactable.GetComponent<SensorBlock>();
    }

    public void DeregisterSensor(XRBaseInteractable interactable)
    {
        var interactableSensor = interactable.GetComponent<SensorBlock>();

        if (interactableSensor.Equals(this.Sensor)) this.Sensor = null;

        Debug.Log("unregisterrrS");
    }

    public void RegisterComparator(XRBaseInteractable interactable)
    {
        Debug.Log("RegisterComparator");
        this.Comparator = interactable.GetComponent<ComparatorBlock>();
        Debug.Log(this.Comparator.GetId());
    }

    public void DeregisterComparator(XRBaseInteractable interactable)
    {
        var interactableComp = interactable.GetComponent<ComparatorBlock>();

        if (interactableComp.Equals(this.Comparator)) this.Comparator = null;

        Debug.Log("unregisterrrC");
    }

    public void RegisterValue(XRBaseInteractable interactable)
    {
        Debug.Log("RegisterValue");
        this.Value = interactable.GetComponent<ValueBlock>();
        Debug.Log(this.Value.GetId());
    }

    public void DeregisterValue(XRBaseInteractable interactable)
    {
        var interactableVal = interactable.GetComponent<ValueBlock>();

        if (interactableVal.Equals(this.Value)) this.Value = null;
        Debug.Log("unregisterrrV");
    }


    public override CodeNode Parse(CodeNode context, CodeNode prev)
    {
        
        var cond = new Condition(this.Sensor.Parse(), this.Value.Parse(), this.Comparator.Parse());
        return new If(context, prev, cond);
    }
}
