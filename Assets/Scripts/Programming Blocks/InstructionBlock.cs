using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionBlock : ProgrammingBlock
{
    [SerializeField]
    private Instruction.ID Id = Instruction.ID.Drop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        var symbol = transform.Find("Symbol");
        var renderer = symbol.GetComponent<Renderer>();
        renderer.sharedMaterial.SetInt("_Instruction", (int) Id);
    }


    public override CodeNode Parse(CodeNode context, CodeNode prev)
    {
        return new Instruction(this.Id, context, prev);
    }
}
