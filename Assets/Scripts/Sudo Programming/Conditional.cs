using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;

using Block;


namespace SudoProgram
{
    public abstract class Conditional : CodeNode
    {
        public Condition Condition { get; protected set; }
        public CodeNode NextIfTrue;


        public Conditional(CodeNode context, CodeNode prev, Condition cond, ProgrammingBlock block) :
            base(context, prev, block)
        {
            this.Condition = cond;
        }


        public override CodeNode GetNext(ActionController robot)
        {
            if (this.NextIfTrue != null && this.Condition.Check(robot)) return this.NextIfTrue;

            return this.Next;
        }


        public abstract override CodeNode AfterBreak();
    }
}
