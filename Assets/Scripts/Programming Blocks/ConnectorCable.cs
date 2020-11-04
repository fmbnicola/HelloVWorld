﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorCable : MonoBehaviour
{
    public Transform AnchorPoint;

    public Transform Plug;

    public LineRenderer Renderer;

    public List<ConnectorNode> Nodes;

    private readonly float Range = 1;

    private GameObject NodeParent;

    private ConnectorNode Head;
    private ConnectorNode Tail;


    // Start is called before the first frame update
    void Start()
    {
        this.Renderer = this.GetComponent<LineRenderer>();

        this.Nodes = new List<ConnectorNode>();

        this.NodeParent = new GameObject("Generated By Connector Cable");
        this.NodeParent.transform.position = Vector3.zero;

        this.Head = this.AddNode(this.AnchorPoint.position);
        this.Tail = this.AddNode(this.Plug.position, this.Head);
    }


    // Update is called once per frame
    void Update()
    {
        #region Head
        var vector = this.Head.transform.position - this.Head.Next.transform.position;

        if (vector.magnitude < this.Range)
        {
            this.Head.transform.position = this.AnchorPoint.position;
        }
        else if (vector.magnitude >= this.Range)
        {
            if (this.Head.Next.Next != null)
            {
                vector = this.Head.transform.position - this.Head.Next.Next.transform.position;

                if (vector.magnitude <= this.Range)
                {
                    // Contract
                    return;
                }
            }

            this.ExtendHead();
        }
        #endregion

        #region Tail
        this.Tail.transform.position = this.Plug.position;

        vector = this.Tail.transform.position - this.Tail.Prev.transform.position;

        if(vector.magnitude < this.Range)
        {
            this.Tail.transform.position = this.Plug.position;
        }
        else if(vector.magnitude >= this.Range)
        {
            if(this.Tail.Prev.Prev != null)
            {
                vector = this.Tail.transform.position - this.Tail.Prev.Prev.transform.position;

                if(vector.magnitude <= this.Range)
                {
                    // Contract
                    return;
                }
            }

            this.ExtendTail();
        }
        #endregion

        this.Renderer.positionCount = this.Nodes.Count;
        this.Renderer.SetPositions(this.GetPositions());
    }


    #region Nodes
    private ConnectorNode CreateNode(Vector3 position)
    {
        var node = new GameObject("CableNode");
        var script = node.AddComponent<ConnectorNode>();

        node.transform.position = position;

        node.transform.parent = this.NodeParent.transform;

        return script;
    }

    private ConnectorNode AddNode(Vector3 position, ConnectorNode prev = null)
    {
        var node = this.CreateNode(position);

        if(prev != null) this.ConnectNodes(prev, node);

        node.Index = this.Nodes.Count;

        this.Nodes.Add(node);

        return node;
    }

    private void InsertNode(int index, ConnectorNode node)
    {
        var count = this.Nodes.Count;

        if (index >= count || node == null) return;

        this.Nodes.Insert(index, node);

        if (index - 1 >=     0) this.ConnectNodes(this.Nodes[index - 1], node);
        if (index + 1 <  count) this.ConnectNodes(node, this.Nodes[index + 1]);

        for(var i = index; i < this.Nodes.Count; i++)
        {
            this.Nodes[i].Index = i;
        }
    }

    private void RemoveNode(int index)
    {
        if (index <= 0 || this.Nodes.Count <= index) return;

        var node = this.Nodes[index];

        var prev = node.Prev;
        var next = node.Next;

        if(prev != null && next != null)
        {
            this.ConnectNodes(prev, next);
        }

        if (this.Tail == node) this.Tail = prev;
    }

    private void ConnectNodes(ConnectorNode first, ConnectorNode second)
    {
        first.Next  = second;
        second.Prev = first;
    }
    #endregion

    #region Extend
    private void ExtendHead()
    {
        var node = this.CreateNode(this.AnchorPoint.position);

        this.InsertNode(0, node);

        this.Head = node;
    }

    private void ExtendTail()
    {
        var node = this.AddNode(this.Plug.position, this.Tail);

        this.Tail = node;
    }
    #endregion

    #region Contract
    private void ContractHead()
    {
        this.RemoveNode(this.Head.Next.Index);
    }

    private void ContractTail()
    {
        this.RemoveNode(this.Tail.Prev.Index);
    }
    #endregion


    public void Clear()
    {
        this.Nodes.Clear();

        this.Head = this.AddNode(this.AnchorPoint.position);
        this.Tail = this.AddNode(this.Plug.position, this.Head);
    }


    public Vector3[] GetPositions()
    {
        var array = new Vector3[this.Nodes.Count];

        var i = 0;

        foreach(var node in this.Nodes)
        {
            array[i++] = node.transform.position;
        }

        return array;
    }
}
