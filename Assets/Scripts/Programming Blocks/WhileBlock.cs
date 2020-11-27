using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using SudoProgram;

namespace Block
{
    public class WhileBlock : ConditionalBlock
    {
        public override CodeNode Parse(CodeNode context, CodeNode prev)
        {
            var cond = this.ParseCondition();

            var whileNode = new While(context, prev, cond, this);

            var nextIfTrue = this.ParseInnerCode(whileNode);

            whileNode.NextIfTrue = nextIfTrue;

            return whileNode;
        }
    }
}
