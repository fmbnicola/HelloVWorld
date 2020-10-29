using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public ProgrammingBlock StartBlock;

    public enum States
    {
        Idle,
        StartUp,
        Active
    }

    public States State { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.State = States.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.State)
        {
            default:
                break;

            case States.StartUp:
                break;

            case States.Active:
                break;
        }
    }


    public void StartUp()
    {
        if (this.State == States.Idle) this.State = States.StartUp;
    }

    public void Save()
    {
        var program = this.Parse();

        //Send to Floppy
    }

    public void Clear()
    {
        //Clear Block Links
    }

    public void ShutDown()
    {
        if(this.State == States.Active)
        {
            this.Clear();

            this.State = States.Idle;
        }
    }


    public CodeNode Parse()
    {
        if(this.StartBlock != null)
        {
            CodeNode head = null;
            CodeNode prev = null;

            var block = this.StartBlock.GetNext();

            while(block != null)
            {
                Debug.Log(block.name);
                var node = block.Parse(null, prev);

                if (head == null) head = node;
                else
                {
                    prev.Next = node;

                }

                prev = node;

                block = block.GetNext();
            }

            return head;
        }

        return null;
    }
}
