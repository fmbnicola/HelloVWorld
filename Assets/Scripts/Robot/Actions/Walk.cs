using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class Walk : Action
    {
        private Vector3 TargetPos { get; set; }

        private float Margin { get; set; }



        public Walk(RobotController robot, Instruction walk, float margin) : base(robot, walk)
        {
            Vector3 pos = robot.GetPosition();
            float zTarget = pos.z + robot.Dimensions.z;

            this.TargetPos = new Vector3(pos.x, pos.y, zTarget);

            this.Margin = margin;
        }



        override public void Execute()
        {
            Debug.Log(base.ProgramLine.ToString() + " -> " + this.ToString());

            // the line is done because the robot does not need to do nothing
        }


        public override bool Completed()
        {
            float dist = Vector3.Distance(this.Robot.GetPosition(), this.TargetPos);

            Debug.Log("Dist: " + dist);

            if (dist <= this.Margin)
            {
                this.ProgramLine.Complete = true;
            }

            return this.ProgramLine.Complete;
        }

    }
}
