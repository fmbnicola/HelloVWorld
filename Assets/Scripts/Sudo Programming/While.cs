using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;

using Block;


namespace SudoProgram
{

    public class While : Conditional
    {
        public While(CodeNode context, CodeNode prev, Condition cond, ProgrammingBlock block) :
            base(context, prev, cond, block)
        { }


        public override CodeNode AfterBreak()
        {
            var inBlock = this.NextIfTrue;

            inBlock.Complete = false;

            while (inBlock.Next != null)
            {
                inBlock = inBlock.Next;

                inBlock.Complete = false;
            }

            return this;
        }
    }
}
