using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Robot
{
    public static class ProgramHelper
    {
        public static CodeNode DebugProgram()
        {
            CodeNode Line1 = new Instruction(Instruction.ID.Walk, null, null);

            CodeNode Line2 = new Instruction(Instruction.ID.Rotate, null, Line1);
            Line1.Next = Line2;

            CodeNode Line3 = new Instruction(Instruction.ID.Walk, null, Line2);
            Line2.Next = Line3;

            CodeNode Line4 = new Instruction(Instruction.ID.Rotate, null, Line3);
            Line3.Next = Line4;

            CodeNode Line5 = new Instruction(Instruction.ID.Walk, null, Line4);
            Line4.Next = Line5;

            CodeNode Line6 = new Instruction(Instruction.ID.Rotate, null, Line5);
            Line5.Next = Line6;

            CodeNode Line7 = new Instruction(Instruction.ID.Walk, null, Line6);
            Line6.Next = Line7;

            CodeNode Line8 = new Instruction(Instruction.ID.Rotate, null, Line7);
            Line7.Next = Line8;

            return Line1;
        }


        public static CodeNode HappyDance()
        {
            CodeNode Line1 = new Instruction(Instruction.ID.Rotate, null, null);

            CodeNode Line2 = new Instruction(Instruction.ID.Rotate, null, Line1);
            Line1.Next = Line2;

            CodeNode Line3 = new Instruction(Instruction.ID.Rotate, null, Line2);
            Line2.Next = Line3;

            CodeNode Line4 = new Instruction(Instruction.ID.Rotate, null, Line3);
            Line3.Next = Line4;

            return Line1;
        }


        public static CodeNode InitialProgramLine()
        {
            return new Instruction(Instruction.ID.Walk, null, null);
        }
    }
}
