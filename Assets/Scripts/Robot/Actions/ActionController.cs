using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class ActionController : MonoBehaviour
    {
        #region /* Action Control */

        private RobotController Robot { get; set; }

        private Action CurrentAction { get; set; }

        #endregion

        #region /* Attributes defined on Editor */

        public float WalkMargin;
        public float Force;
        public float MaxSpeed;

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
        
        public void Initialize(RobotController Robot)
        {
            this.Robot = Robot;
        }
        
        #endregion



        #region === Action Methods ===

        public bool CurrentActionCompleted()
        {
            if (this.CurrentAction != null)
            {
                return this.CurrentAction.Completed();
            }

            return true;
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
                    this.CurrentAction = new Walk(this.Robot, programLine, this.WalkMargin, this.Force, this.MaxSpeed);
                    break;
                
                case Instruction.ID.Grab:
                    this.CurrentAction = new Grab(this.Robot, programLine);
                    break;
                
                case Instruction.ID.Drop:
                    this.CurrentAction = new Drop(this.Robot, programLine);
                    break;
                
                case Instruction.ID.Rotate:
                    this.CurrentAction = new Rotate(this.Robot, programLine);
                    break;

                default:
                    throw new System.Exception("Unknow Instruction");
            }

            this.CurrentAction.Execute();
        }


        public void Execute(CodeNode programLine)
        {
            this.CurrentAction = new Action(this.Robot, programLine);

            this.CurrentAction.Execute();
        }

        #endregion
    }
}
