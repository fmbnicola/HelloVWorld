using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionBlock : ProgrammingBlock
{
    [SerializeField]
    private Instruction.ID Id;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    override public CodeNode Parse(CodeNode context, CodeNode prev)
    {
        return new Instruction(this.Id, context, prev);
    }
}
