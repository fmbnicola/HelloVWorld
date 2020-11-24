using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IfBlock : ConditionalBlock
{ 
    public override CodeNode Parse(CodeNode context, CodeNode prev)
    {
        var cond = this.ParseCondition();

        var ifNode = new If(context, prev, cond, this);

        var nextIfTrue = this.ParseInnerCode(ifNode);

        ifNode.NextIfTrue = nextIfTrue;

        return ifNode;
    }
}
