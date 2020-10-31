using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;
using UnityEngine.XR.Interaction.Toolkit;

namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        #region /* Actions Attributes */

        private ActionController ActionController { get; set; }

        #endregion

        #region /* Progam Attributes */

        private XRSocketInteractor DiskSocket { get; set; }

        private FloppyDisk Disk { get; set; }

        private CodeNode Program { get; set; }
        private bool ProgramRunning { get; set; }

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.ActionController = this.transform.GetComponent<ActionController>();

            this.DiskSocket = this.transform.GetComponentInChildren<XRSocketInteractor>();
            this.DiskSocket.onSelectEnter.AddListener((disk) => this.LoadProgram(disk));
            this.DiskSocket.onSelectExit.AddListener((disk) => this.CloseProgram());

            this.Program = null;
            this.ProgramRunning = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (this.ProgramRunning)
            {
                this.ExecuteProgram();
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


        private void LoadProgram(XRBaseInteractable disk)
        {
            this.Disk = disk.GetComponent<FloppyDisk>();
            this.Program = this.Disk.codeHead;

            // TODO: remove if
            if (this.Program == null)
            {
                this.createProgram();
            }

            Debug.Log("Program loaded");

            // TODO: only start execution after clicking the start button
            this.ProgramRunning = true;
            this.Program.Execute(this.ActionController);
        }


        private void CloseProgram()
        {
            this.Disk = null;
            this.Program = null;
            this.ProgramRunning = false;

            Debug.Log("Program info cleaned");
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
                    this.ProgramRunning = false;
                    this.Program = this.Disk.codeHead;
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