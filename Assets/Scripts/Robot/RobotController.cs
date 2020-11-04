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

        public float Height;

        #endregion

        #region /* Actions Attributes */

        private ActionController ActionController { get; set; }

        #endregion

        #region /* Progam Attributes */

        private XRSocketInteractor DiskSocket { get; set; }
        private FloppyDisk Disk { get; set; }

        private CodeNode Program { get; set; }
        private bool ProgramRunning { get; set; }
        private bool HappyDance { get; set; }

        public bool DebugProgram;
        public bool DebugInfo;

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
            this.Rigidbody.centerOfMass = new Vector3(0, -this.Height, 0);
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
            this.HappyDance = false;
        }


        private void DoHappyDance()
        {
            this.HappyDance = true;

            this.Program = ProgramHelper.HappyDance();
            this.Program.Execute(this.ActionController);

            if (this.DebugInfo)
            {
                Debug.Log("Happy Dance Started");
            }
        }


        private void LoadProgram(XRBaseInteractable disk)
        {
            this.Disk = disk.GetComponent<FloppyDisk>();

            if (this.Disk != null)
            {
                this.Program = this.Disk.codeHead;

                if (this.DebugProgram && this.Program == null)
                {
                    this.Program = ProgramHelper.DebugProgram();
                }

                if (this.DebugInfo)
                {
                    Debug.Log("Program Loaded");
                }
            }
        }


        public void StartProgram()
        {
            if (this.Disk != null && this.Program != null && !this.ProgramRunning)
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
                this.HappyDance = false;

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
                    if (this.HappyDance)
                    {
                        if (this.DebugInfo)
                        {
                            Debug.Log("Happy Dance Ended");
                        }

                        this.ProgramRunning = false;
                        this.HappyDance = false;
                        this.Program = this.Disk.codeHead;

                        if (this.DebugProgram && this.Program == null)
                        {
                            this.Program = ProgramHelper.DebugProgram();
                        }
                    }
                    else
                    {
                        if (this.DebugInfo)
                        {
                            Debug.Log("Program Ended");
                        }

                        this.DoHappyDance();
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