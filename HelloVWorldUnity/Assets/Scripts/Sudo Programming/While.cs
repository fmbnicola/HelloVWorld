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
            this.DeConfirm();

            return this;
        }


        public override void DeConfirm()
        {
            var inBlock = this.NextIfTrue;

            while (true)
            {
                if (inBlock is If || inBlock is While) inBlock.AfterBreak();

                inBlock.Complete = false;

                inBlock = inBlock.Next;

                if (inBlock == null) break;
            }
        }
    }
}
