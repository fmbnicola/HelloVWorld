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

            Vector3 robotPos = robot.GetPosition();
            Vector3 moveDir = robot.GetFoward();
            this.TargetPos = robotPos + moveDir * this.StepSize;

            this.RobotBody = this.Robot.Rigidbody;

            if (this.Robot.DebugInfo)
            {
                Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());

                //GameObject.Find("Debug").transform.position = this.TargetPos;
            }
        }



        public override void Execute()
        {
            this.RobotBody.AddRelativeForce(Vector3.forward * this.Force);

            if (this.RobotBody.velocity.magnitude > this.MaxSpeed)
            {
                this.RobotBody.velocity = this.RobotBody.velocity.normalized * this.MaxSpeed; 
            }

            this.AnimationController.ThreadsFoward();
        }


        public override bool Completed()
        {
            float dist = Vector3.Distance(this.Robot.GetPosition(), this.TargetPos);

            if (dist < this.Margin)
            {
                this.ProgramLine.Complete = true;

                this.Terminate();
            }

            return this.ProgramLine.Complete;
        }


        public override void Terminate()
        {
            this.RobotBody.velocity = Vector3.zero;
            
            this.AnimationController.ThreadsStop();
        }

    }
}
