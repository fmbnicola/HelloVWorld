using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;

namespace Robot.Actions
{
    public class HappyDance : Action
    {
        public HappyDance(RobotController robot, End end) : base(robot, end)
        {
            this.ProgramLine.Next = ProgramHelper.HappyDance(this.ProgramLine.Block);
        }



        public override void Execute()
        {
            if (this.Robot.DebugInfo)
            {
                Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());
            }

            this.AnimationController.FaceExcited(4);

            this.ProgramLine.Complete = true;
        }

    }
}
