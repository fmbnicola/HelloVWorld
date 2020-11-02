using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class Walk : Action
    {
        private float StepSize { get; set; }
        private float MaxSpeed { get; set; }
        private float Force { get; set; }
        private float Margin { get; set; }

        private Vector3 TargetPos { get; set; }

        private Rigidbody RobotBody { get; set; }



        public Walk(RobotController robot, Instruction walk, float stepSize, float maxSpeed, float force, float margin) : 
            base(robot, walk)
        {
            this.StepSize = stepSize;
            this.MaxSpeed = maxSpeed;
            this.Force = force;
            this.Margin = margin;

            Vector3 pos = robot.GetPosition();
            float zTarget = pos.z + this.StepSize;
            this.TargetPos = new Vector3(pos.x, pos.y, zTarget);

            this.RobotBody = this.Robot.Rigidbody;
            this.RobotBody.useGravity = false;

            Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());
        }



        public override void Execute()
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


            if (dist < this.Margin)
            {
                this.ProgramLine.Complete = true;

                this.Terminate();

                Debug.Log("Walk End");
            }

            return this.ProgramLine.Complete;
        }


        public override void Terminate()
        {
            this.RobotBody.velocity = Vector3.zero;
            this.RobotBody.useGravity = true;
        }

    }
}
