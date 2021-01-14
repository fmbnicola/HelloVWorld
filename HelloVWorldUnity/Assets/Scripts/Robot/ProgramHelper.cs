using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;
using Block;

namespace Robot
{
    public static class ProgramHelper
    {
        public static CodeNode DebugProgram()
        {
            CodeNode Line1 = new Instruction(Instruction.ID.Walk, null, null, null);

            CodeNode Line2 = new Instruction(Instruction.ID.TurnLeft, null, Line1, null);
            Line1.Next = Line2;

            CodeNode Line3 = new Instruction(Instruction.ID.Walk, null, Line2, null);
            Line2.Next = Line3;

            CodeNode Line4 = new Instruction(Instruction.ID.TurnLeft, null, Line3, null);
            Line3.Next = Line4;

            CodeNode Line5 = new Instruction(Instruction.ID.Walk, null, Line4, null);
            Line4.Next = Line5;

            CodeNode Line6 = new Instruction(Instruction.ID.TurnLeft, null, Line5, null);
            Line5.Next = Line6;

            CodeNode Line7 = new Instruction(Instruction.ID.Walk, null, Line6, null);
            Line6.Next = Line7;

            CodeNode Line8 = new Instruction(Instruction.ID.TurnLeft, null, Line7, null);
            Line7.Next = Line8;

            CodeNode Line9 = new End(null, Line8, null);
            Line8.Next = Line9;

            return Line1;
        }


        public static CodeNode HappyDance(ProgrammingBlock end)
        {
            CodeNode Line1 = new Instruction(Instruction.ID.TurnRight, null, null, end);

            CodeNode Line2 = new Instruction(Instruction.ID.TurnRight, null, Line1, end);
            Line1.Next = Line2;

            CodeNode Line3 = new Instruction(Instruction.ID.TurnRight, null, Line2, end);
            Line2.Next = Line3;

            CodeNode Line4 = new Instruction(Instruction.ID.TurnRight, null, Line3, end);
            Line3.Next = Line4;

            return Line1;
        }

        public static CodeNode HappyDanceLeft(ProgrammingBlock end)
        {
            CodeNode Line1 = new Instruction(Instruction.ID.TurnLeft, null, null, end);

            CodeNode Line2 = new Instruction(Instruction.ID.TurnLeft, null, Line1, end);
            Line1.Next = Line2;

            CodeNode Line3 = new Instruction(Instruction.ID.TurnLeft, null, Line2, end);
            Line2.Next = Line3;

            CodeNode Line4 = new Instruction(Instruction.ID.TurnLeft, null, Line3, end);
            Line3.Next = Line4;

            return Line1;
        }


        public static CodeNode InitialProgramLine()
        {
            return new Instruction(Instruction.ID.Walk, null, null, null);
        }
    }
}
