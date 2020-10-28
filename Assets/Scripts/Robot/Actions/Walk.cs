using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class Walk : Action
    {
        public Walk(Transform robot, Instruction walk) : base(robot, walk) { }


        override public void Execute()
        {
            Debug.Log(base.ProgramLine.ToString() + " -> " + this.ToString());

            // the line is done because the robot does not need to do nothing
            this.ProgramLine.Complete = true;
        }

    }
}
