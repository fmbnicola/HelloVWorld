using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Sensors;



namespace Robot.Actions
{
    public class ActionController : MonoBehaviour
    {
        #region /* Action Control */

        private RobotController Robot { get; set; }
        private SensorController Sensors { get; set; }

        private Action CurrentAction { get; set; }

        #endregion

        #region /* Attributes defined on Editor */

        #region Walk
        public float StepSize;
        public float MaxWalkSpeed;
        public float WalkForce;
        public float WalkMargin;
        #endregion

        #region Rotate
        public float RotationAngle;
        public float MaxRotationSpeed;
        public float RotationTorque;
        public float RotationMargin;
        #endregion

        #endregion




        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion



        #region === Initialization ===
        
        public void Initialize(RobotController Robot, SensorController sensor)
        {
            this.Robot = Robot;
            this.Sensors = sensor;
        }
        
        #endregion



        #region === Action Methods ===

        public bool ActionCompleted()
        {
            if (this.CurrentAction != null)
            {
                return this.CurrentAction.Completed();
            }

            return true;
        }


        public void AbortAction()
        {
            if (this.CurrentAction != null)
            {
                this.CurrentAction.Terminate();
            }
        }


        public void Continue()
        {
            this.CurrentAction.Execute();
        }


        public void Execute(Instruction programLine)
        {
            switch (programLine.Id)
            {
                case Instruction.ID.Walk:
                    this.CurrentAction = new Walk(this.Robot, programLine, this.StepSize, this.MaxWalkSpeed, this.WalkForce, this.WalkMargin);
                    break;
                
                case Instruction.ID.Grab:
                    this.CurrentAction = new Grab(this.Robot, programLine);
                    break;
                
                case Instruction.ID.Drop:
                    this.CurrentAction = new Drop(this.Robot, programLine);
                    break;
                
                case Instruction.ID.TurnRight:
                    this.CurrentAction = new Rotate(this.Robot, programLine, this.RotationAngle, this.MaxRotationSpeed, this.RotationTorque, this.RotationMargin);
                    break;

                case Instruction.ID.TurnLeft:
                    this.CurrentAction = new Rotate(this.Robot, programLine, -this.RotationAngle, this.MaxRotationSpeed, -this.RotationTorque, this.RotationMargin);
                    break;

                default:
                    throw new System.Exception("Unknow Instruction");
            }

            this.CurrentAction.Execute();
        }


        public void Execute(End programLine)
        {
            this.CurrentAction = new HappyDance(this.Robot, programLine);
        }


        public void Execute(CodeNode programLine)
        {
            this.CurrentAction = new Action(this.Robot, programLine);

            this.CurrentAction.Execute();
        }

        #endregion



        public Value Sense(Sensor.ID sensor)
        {
            return this.Sensors.Sense(sensor);
        }
    }
}
