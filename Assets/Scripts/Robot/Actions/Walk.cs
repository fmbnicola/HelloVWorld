using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class Walk : Action
    {
        private Vector3 TargetPos { get; set; }

        private float Margin { get; set; }
        private float Force { get; set; }
        private float MaxSpeed { get; set; }

        private Rigidbody RobotBody { get; set; }



        public Walk(RobotController robot, Instruction walk, float margin, float force, float maxSpeed) : 
            base(robot, walk)
        {
            Vector3 pos = robot.GetPosition();
            float zTarget = pos.z + robot.Dimensions.z;
            this.TargetPos = new Vector3(pos.x, pos.y, zTarget);

            this.Margin = margin;
            this.Force = force;
            this.MaxSpeed = maxSpeed;

            this.RobotBody = this.Robot.Rigidbody;
            this.RobotBody.useGravity = false;

            Debug.Log(base.ProgramLine.ToString() + " -> " + this.ToString());
        }



        override public void Execute()
        {
            this.RobotBody.AddRelativeForce(Vector3.forward * this.Force);

            if (this.RobotBody.velocity.magnitude > this.MaxSpeed)
            {
                this.RobotBody.velocity = this.RobotBody.velocity.normalized * this.MaxSpeed; 
            }
        }


        public override bool Completed()
        {
            float dist = Vector3.Distance(this.Robot.GetPosition(), this.TargetPos);


            if (dist <= this.Margin)
            {
                this.ProgramLine.Complete = true;

                this.RobotBody.velocity = Vector3.zero;
                this.RobotBody.useGravity = true;

                Debug.Log("Target Reached");
            }

            return this.ProgramLine.Complete;
        }

    }
}
