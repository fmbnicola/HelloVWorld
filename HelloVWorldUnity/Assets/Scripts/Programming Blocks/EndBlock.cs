﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;

namespace Block
{
    public class EndBlock : ProgrammingBlock
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public override CodeNode Parse(CodeNode context, CodeNode prev)
        {
            return new End(context, prev, this);
        }
    }
}
