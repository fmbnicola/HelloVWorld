using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class Rotate : Action
    {
        private float Angle { get; set; }
        private float MaxSpeed { get; set; }
        private float Torque { get; set; }
        private float Margin { get; set; }

        private Vector3 Target { get; set; }

        private Rigidbody RobotBody { get; set; }



        public Rotate(RobotController robot, Instruction rotate, float angle, float maxSpeed, float torque, float margin) :
            base(robot, rotate)
        {
            this.Angle = angle;
            this.MaxSpeed = maxSpeed;
            this.Torque = torque;
            this.Margin = margin;

            Vector3 ori = this.Robot.GetRotation();
            float yTarget = (ori.y + this.Angle) % 360;
            this.Target = new Vector3(ori.x, yTarget, ori.z);

            this.RobotBody = this.Robot.Rigidbody;

            if (this.Robot.DebugInfo)
            {
                Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());
            }
        }



        override public void Execute()
        {
            this.RobotBody.AddRelativeTorque(Vector3.up * this.Torque);

            if (this.RobotBody.angularVelocity.magnitude > this.MaxSpeed)
            {
                this.RobotBody.angularVelocity = this.RobotBody.angularVelocity.normalized * this.MaxSpeed;
            }
        }


        public override bool Completed()
        {
            float dist = Vector3.Distance(this.Robot.GetRotation(), this.Target);

            if (dist < this.Margin)
            {
                this.ProgramLine.Complete = true;

                this.Terminate();
            }

            return this.ProgramLine.Complete;
        }


        public override void Terminate()
        {
            this.RobotBody.angularVelocity = Vector3.zero;
        }

    }
}
