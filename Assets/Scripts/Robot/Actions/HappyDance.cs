﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class HappyDance : Action
    {
        public HappyDance(RobotController robot, End end) : base(robot, end) { }



        public override void Execute()
        {
            if (this.Robot.DebugInfo)
            {
                Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());
            }

            this.AnimationController.FaceExcited();

            this.ProgramLine.Next = ProgramHelper.HappyDance();

            this.ProgramLine.Complete = true;
        }

    }
}
