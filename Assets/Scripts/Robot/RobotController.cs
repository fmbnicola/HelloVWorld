using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;
using UnityEngine.XR.Interaction.Toolkit;



namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        #region /* Robot Info */

        public Rigidbody Rigidbody { get; private set; }

        public bool DebugInfo;
        
        #endregion

        #region /* Actions Attributes */

        private ActionController ActionController { get; set; }

        #endregion

        #region /* Progam Attributes */

        private XRSocketInteractor DiskSocket { get; set; }
        private FloppyDisk Disk { get; set; }

        private CodeNode Program { get; set; }
        private bool ProgramRunning { get; set; }

        private StartButton StartButton { get; set; }

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.InitializeRobotInfo();

            this.PrepareActionController();

            this.PrepareForProgram();
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            if (this.ProgramRunning)
            {
                this.ExecuteProgram();
            }
        }

        #endregion



        #region === Robot Info Methods ===

        private void InitializeRobotInfo()
        {
            this.Rigidbody = this.transform.GetComponent<Rigidbody>();
        }


        public Vector3 GetPosition()
        {
            return this.transform.position;
        }


        public Vector3 GetRotation()
        {
            return this.transform.rotation.eulerAngles;
        }


        public Vector3 GetFoward()
        {
            return this.transform.forward;
        }

        #endregion



        #region === Action Methods ===

        private void PrepareActionController()
        {
            this.ActionController = this.transform.GetComponent<ActionController>();
            this.ActionController.Initialize(this);
        }

        #endregion



        #region === Program Methods ===

        private void PrepareForProgram()
        {
            this.DiskSocket = this.transform.GetComponentInChildren<XRSocketInteractor>();
            this.DiskSocket.onSelectEnter.AddListener((disk) => this.LoadProgram(disk));
            this.DiskSocket.onSelectExit.AddListener((disk) => this.AbortProgram(disk));

            this.Program = null;
            this.ProgramRunning = false;

            this.StartButton = this.transform.GetComponentInChildren<StartButton>();
            this.StartButton.Initialize(this);
        }


        private void createProgram()
        {
            CodeNode Line1 = new Instruction(Instruction.ID.Walk, null, null);

            CodeNode Line2 = new Instruction(Instruction.ID.Rotate, null, Line1);
            Line1.Next = Line2;

            CodeNode Line3 = new Instruction(Instruction.ID.Walk, null, Line2);
            Line2.Next = Line3;

            CodeNode Line4 = new Instruction(Instruction.ID.Rotate, null, Line3);
            Line3.Next = Line4;
            
            CodeNode Line5 = new Instruction(Instruction.ID.Walk, null, Line4);
            Line4.Next = Line5;

            CodeNode Line6 = new Instruction(Instruction.ID.Rotate, null, Line5);
            Line5.Next = Line6;

            CodeNode Line7 = new Instruction(Instruction.ID.Walk, null, Line6);
            Line6.Next = Line7;

            CodeNode Line8 = new Instruction(Instruction.ID.Rotate, null, Line7);
            Line7.Next = Line8;

            this.Program = Line1;
        }


        private void LoadProgram(XRBaseInteractable disk)
        {
            this.Disk = disk.GetComponent<FloppyDisk>();

            if (this.Disk != null)
            {
                this.Program = this.Disk.codeHead;

                // TODO: remove if
                if (this.Program == null)
                {
                    this.createProgram();
                }

                if (this.DebugInfo)
                {
                    Debug.Log("Program Loaded");
                }
            }
        }


        public void StartProgram()
        {
            if (this.Disk != null && this.Program != null)
            {
                if (this.DebugInfo)
                {
                    Debug.Log("Progam Started");
                }

                this.ProgramRunning = true;
                this.Program.Execute(this.ActionController);
            }
        }


        private void AbortProgram(XRBaseInteractable disk)
        {
            FloppyDisk floppy = disk.transform.GetComponent<FloppyDisk>();

            if (this.Disk == floppy)
            {
                this.ActionController.AbortAction();

                this.Disk = null;
                this.Program = null;
                this.ProgramRunning = false;

                if (this.DebugInfo)
                {
                    Debug.Log("Program Aborted");
                }
            }
        }


        private void ExecuteProgram()
        {
            if (this.ActionController.ActionCompleted())
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

                    // TODO: remove if
                    if (this.Program == null)
                    {
                        this.createProgram();
                    }

                    if (this.DebugInfo)
                    {
                        Debug.Log("Program Ended");
                    }
                }
            }
            else
            {
                this.ActionController.Continue();
            }
        }

        #endregion

    }
}