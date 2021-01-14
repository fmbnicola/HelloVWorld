using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;

using Block;


namespace SudoProgram
{

    public class End : CodeNode
    {
        public End(CodeNode context, CodeNode prev, ProgrammingBlock block) :
            base(context, prev, block)
        { }



        public override bool Execute(ActionController robotActuator)
        {
            robotActuator.Execute(this);

            return this.Complete;
        }

    }
}
