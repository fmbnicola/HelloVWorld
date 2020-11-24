using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionBlock : ProgrammingBlock
{
    [SerializeField]
    private Instruction.ID Id = Instruction.ID.Drop;

    private MaterialPropertyBlock propertyBlock;

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
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetInt("_Instruction", (int)Id);

        var symbol = transform.Find("Symbol");
        var renderer = symbol.GetComponent<Renderer>();
        renderer.SetPropertyBlock(propertyBlock);
    }


    public override CodeNode Parse(CodeNode context, CodeNode prev)
    {
        return new Instruction(this.Id, context, prev, this);
    }
}
