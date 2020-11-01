using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class Grab : Action
    {
        public Grab(RobotController robot, Instruction grab) : base(robot, grab) { }


        override public void Execute()
        {
            Debug.Log(base.ProgramLine.ToString() + " -> " + this.ToString());

            // the line is done because the robot does not need to do nothing
            this.ProgramLine.Complete = true;
        }

    }
}
