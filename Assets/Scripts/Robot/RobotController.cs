using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        private ActionController ActionController { get; set; }


        private CodeNode Program { get; set; }

        private bool ProgramEnded { get; set; }



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.ActionController = this.transform.GetComponent<ActionController>();

            this.Program = null;
            this.ProgramEnded = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (this.Program != null)
            {
                this.ExecuteProgram();
            }
        }


        private void OnTriggerStay(Collider other)
        {
            if (this.Program == null && other.CompareTag("Disk") && !this.ProgramEnded)
            {
                this.LoadProgram(other.gameObject);
            }
        }

        #endregion



        #region === Program Methods ===

        private void createProgram()
        {
            CodeNode Line1 = new CodeNode(null, null);

            CodeNode Line2 = new CodeNode(null, Line1);
            Line1.Next = Line2;

            CodeNode Line3 = new Instruction(Instruction.ID.Walk, null, Line2);
            Line2.Next = Line3;

            CodeNode Line4 = new Instruction(Instruction.ID.Grab, null, Line3);
            Line3.Next = Line4;

            CodeNode Line5 = new Instruction(Instruction.ID.Drop, null, Line4);
            Line4.Next = Line5;

            CodeNode Line6 = new Instruction(Instruction.ID.Rotate, null, Line5);
            Line5.Next = Line6;

            this.Program = Line1;
        }

        public void LoadProgram(GameObject disk)
        {
            // TODO: get program head from floppy disk
            this.createProgram();
            Debug.Log("Program loaded");

            // TODO: only start execution after clicking the start button
            this.Program.Execute(this.ActionController);
        }


        private void ExecuteProgram()
        {
            if (this.ActionController.CurrentActionCompleted())
            {
                this.Program = this.Program.Next;

                if (this.Program != null)
                {
                    this.Program.Execute(this.ActionController);
                }
                else
                {
                    this.ProgramEnded = true;
                    Debug.Log("Program ended");
                }
            }
            else
            {
                // TODO: continue current action
            }
        }

        #endregion

    }
}