using System.Collections;
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
        this.Sensor = interactable.GetComponent<SensorBlock>();
        Debug.Log(this.Sensor.GetId());
    }

    public void DeregisterSensor(XRBaseInteractable interactable)
    {
        var interactableSensor = interactable.GetComponent<SensorBlock>();

        if (interactableSensor.Equals(this.Sensor)) this.Sensor = null;

    }

    public void RegisterComparator(XRBaseInteractable interactable)
    {
        this.Comparator = interactable.GetComponent<ComparatorBlock>();
        Debug.Log(this.Comparator.GetId());
    }

    public void DeregisterComparator(XRBaseInteractable interactable)
    {
        var interactableComp = interactable.GetComponent<ComparatorBlock>();

        if (interactableComp.Equals(this.Comparator)) this.Comparator = null;
    }

    public void RegisterValue(XRBaseInteractable interactable)
    {
        this.Value = interactable.GetComponent<ValueBlock>();
        Debug.Log(this.Value.GetId());
    }

    public void DeregisterValue(XRBaseInteractable interactable)
    {
        var interactableVal = interactable.GetComponent<ValueBlock>();

        if (interactableVal.Equals(this.Value)) this.Value = null;
    }


    public override ProgrammingBlock GetNext()
    {
        var plugObj = this.transform.Find("Plug");

        if (plugObj != null)
        {
            var plug = plugObj.GetComponent<Plug>();

            var connectedTo = plug.GetConnectedTo();

            if(connectedTo != null)
            {
                return connectedTo.GetBlock();
            }
        }

        return null;
    }

    public ProgrammingBlock GetNextIfTrue()
    {
        var plugObj = this.transform.Find("PlugTrue");

        if (plugObj != null)
        {
            var plug = plugObj.GetComponent<Plug>();

            var connectedTo = plug.GetConnectedTo();

            if (connectedTo != null)
            {
                return connectedTo.GetBlock();
            }
        }

        return null;
    }


    public CodeNode ParseInnerCode(CodeNode context)
    {
        var firstBlock = this.GetNextIfTrue();
        var currentBlock = firstBlock;

        var firstNode = firstBlock.Parse(context, context);
        var currentNode = firstNode;

        currentBlock = currentBlock.GetNext();

        while(currentBlock != null)
        {
            var newNode = currentBlock.Parse(context, currentNode);

            currentNode.Next = newNode;
            currentNode = newNode;

            currentBlock = currentBlock.GetNext();
        }

        return firstNode;
    }


    public override CodeNode Parse(CodeNode context, CodeNode prev)
    {
        var cond = new Condition(this.Sensor.Parse(), this.Value.Parse(), this.Comparator.Parse());

        var ifNode = new If(context, prev, cond);

        var nextIfTrue = this.ParseInnerCode(ifNode);

        ifNode.NextIfTrue = nextIfTrue;

        return ifNode;
    }
}
