using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WhileBlock : ConditionalBlock
{ 
    public override CodeNode Parse(CodeNode context, CodeNode prev)
    {
        var cond = this.ParseCondition();

        var whileNode = new While(context, prev, cond);

        var nextIfTrue = this.ParseInnerCode(whileNode);

        whileNode.NextIfTrue = nextIfTrue;

        return whileNode;
    }
}
