using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;

using Block;


namespace SudoProgram
{
    public class If : Conditional
    {
        public If(CodeNode context, CodeNode prev, Condition cond, ProgrammingBlock block) :
            base(context, prev, cond, block)
        {
            this.Condition = cond;
        }


        public override CodeNode AfterBreak()
        {
            return this.Next;
        }
    }
}
