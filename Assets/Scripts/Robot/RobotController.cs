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

        public void LoadProgram(GameObject disk)
        {
            // TODO: get program head from floppy disk
            this.Program = new CodeNode(null, null);
            this.Program.Next = new CodeNode(null, this.Program);
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