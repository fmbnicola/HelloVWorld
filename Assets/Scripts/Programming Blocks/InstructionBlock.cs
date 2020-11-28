using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;

namespace Block
{
    public class InstructionBlock : ProgrammingBlock
    {
        [SerializeField]
        private Instruction.ID Id = Instruction.ID.Drop;

        private MaterialPropertyBlock PropertyBlock;

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
            if (this.PropertyBlock == null)
                this.PropertyBlock = new MaterialPropertyBlock();

            this.PropertyBlock.SetInt("_Instruction", (int)Id);

            var symbol = transform.Find("Symbol");
            var renderer = symbol.GetComponent<Renderer>();
            renderer.SetPropertyBlock(this.PropertyBlock);
        }


        public override CodeNode Parse(CodeNode context, CodeNode prev)
        {
            return new Instruction(this.Id, context, prev, this);
        }

        public void Convert(Instruction.ID newType)
        {
            this.Id = newType;

            this.OnValidate();
        }

        public Instruction.ID GetId()
        {
            return this.Id;
        }
    }

}