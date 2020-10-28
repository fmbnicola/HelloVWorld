using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class ActionController : MonoBehaviour
    {
        private Action CurrentAction { get; set; }




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



        #region === Action Methods ===

        public bool CurrentActionCompleted()
        {
            if (this.CurrentAction != null)
            {
                return this.CurrentAction.Completed();
            }

            return true;
        }


        public void Execute(Instruction programLine)
        {
            switch (programLine.Id)
            {
                case Instruction.ID.Walk:
                    this.CurrentAction = new Walk(this.transform, programLine);
                    break;
                
                case Instruction.ID.Grab:
                    this.CurrentAction = new Grab(this.transform, programLine);
                    break;
                
                case Instruction.ID.Drop:
                    this.CurrentAction = new Drop(this.transform, programLine);
                    break;
                
                case Instruction.ID.Rotate:
                    this.CurrentAction = new Rotate(this.transform, programLine);
                    break;

                default:
                    throw new System.Exception("Unknow Instruction");
            }

            this.CurrentAction.Execute();
        }


        public void Execute(CodeNode programLine)
        {
            this.CurrentAction = new Action(this.transform, programLine);

            this.CurrentAction.Execute();
        }

        #endregion
    }
}
