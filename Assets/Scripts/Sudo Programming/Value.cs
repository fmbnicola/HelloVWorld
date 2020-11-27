﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Block;


namespace SudoProgram
{
    public class Value
    {
        public enum ID
        {
            Empty,
            Box,
            Wall,
        }

        public ID Id { get; protected set; }

        public Value(ID id)
        {
            this.Id = id;
        }
    }
}
