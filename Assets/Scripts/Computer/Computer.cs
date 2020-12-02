using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

using SudoProgram;
using Block;
using ComputerElements;



public class Computer : MonoBehaviour
{
    public BlockManager BlockManager;

    public ProgrammingBlock StartBlock;

    public Dais Dais;

    private ComputerAnimator Animator;

    public StartMechanism StartMechanism;

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


    #region Button Call Backs
    public void StartUp()
    {
        if (this.State == States.Idle)
        {
            this.State = States.StartUp;

            this.StartTime = Time.time;

            this.Animator.TurnOn();

            this.BlockManager.State = BlockManager.States.Categories;
        }
    }


    public void Save()
    {
        var program = this.Parse();

        this.StartMechanism.LoadProgram(program);
    }


    public void Clear()
    {
        this.Dais.TurnOff();
    }

    public void ShutDown()
    {
        if(this.State == States.Active)
        {
            this.Clear();

            this.StoreBlocks();

            this.State = States.Idle;

            this.Animator.TurnOff();

            this.BlockManager.State = BlockManager.States.Off;
        }
    }
    #endregion


    public void StoreBlocks()
    {
        var bodies = this.Dais.GetBodies();

        var size = bodies.Count;

        for(var i = 0; i < size; i++)
        {
            var body = bodies[0];

            if(body.CompareTag("Block"))
                this.BlockManager.DeSpawn(body.gameObject);

            bodies.RemoveAt(0);
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

            return head;
        }

        return null;
    }
}
