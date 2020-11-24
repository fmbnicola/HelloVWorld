using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

using Robot;



public class Computer : MonoBehaviour
{
    public ProgrammingBlock StartBlock;

    public Dais Dais;

    public RobotController Robot;

    private ComputerAnimator Animator;

    public enum States
    {
        Idle,
        StartUp,
        Active
    }

    public States State { get; private set; }

    private float StartTime;

    // Start is called before the first frame update
    void Start()
    {
        this.State = States.Idle;

        this.Dais = this.transform.Find("Dais").GetComponent<Dais>();

        this.Animator = this.GetComponent<ComputerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.State)
        {
            default:
                break;

            case States.StartUp:
                if (Time.time - this.StartTime >= 3)
                    this.State = States.Active;
                break;

            case States.Active:
                break;
        }


    }


    public void StartUp()
    {
        if (this.State == States.Idle)
        {
            this.State = States.StartUp;

            this.StartTime = Time.time;

            this.Animator.TurnOn();
        }
    }

    public void Save()
    {
        var program = this.Parse();

        //Send to Floppy
    }

    public void Clear()
    {
        //Clear Block Links

        this.Dais.Release();
    }

    public void ShutDown()
    {
        if(this.State == States.Active)
        {
            this.Clear();

            this.State = States.Idle;

            this.Animator.TurnOff();
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

                var node = block.Parse(null, prev);

                if (head == null) head = node;
                else
                {
                    prev.Next = node;

                }

                prev = node;

                block = block.GetNext();
            }

            if (this.Robot != null)
            {
                this.Robot.LoadProgram(head);
            }
            else Debug.Log("No robot associated");
            return head;
        }

        return null;
    }
}
