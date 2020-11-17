using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;
using Robot.Actions;
using Robot.Sensors;



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
        private SensorController SensorController { get; set; }

        #endregion

        #region /* Progam Attributes */

        private XRSocketInteractor DiskSocket { get; set; }
        private FloppyDisk Disk { get; set; }

        private CodeNode Program { get; set; }
        private bool ProgramRunning { get; set; }
        private bool InStartPosition { get; set; }

        public bool DebugProgram;
        public bool DebugInfo;

        #endregion

        #region /* Animation Attributes */
        
        public RobotAnimationController AnimationController { get; private set; }
        
        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.InitializeRobotInfo();

            this.PrepareActionController();

            this.PrepareForProgram();

            this.PrepareAnimationController();
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


        public void SummonRobot(Vector3 desiredPos, Vector3 desiredRotation)
        {
            this.ResetProgram();

            this.transform.position = desiredPos;
            this.transform.rotation = Quaternion.Euler(desiredRotation);
        }

        #endregion



        #region === Action Methods ===

        private void PrepareActionController()
        {
            this.ActionController = this.transform.GetComponent<ActionController>();
            this.SensorController = this.transform.GetComponentInChildren<SensorController>();

            this.ActionController.Initialize(this, this.SensorController);
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
            this.InStartPosition = false;
        }


        public void AtStartPosition(Vector3 startPos, Vector3 startRot)
        {
            this.SummonRobot(startPos, startRot);
            this.InStartPosition = true;
            this.StartProgram();
        }


        public void LeaveStartPosition()
        {
            this.InStartPosition = false;
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

                this.AnimationController.FaceExcited();

                if (this.DebugInfo)
                {
                    Debug.Log("Program Loaded");
                }

                if (this.InStartPosition)
                {
                    this.StartProgram();
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

                CodeNode firstLine = ProgramHelper.InitialProgramLine();
                firstLine.Next = this.Program;
                this.Program = firstLine;

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

                this.AnimationController.FaceSad();
            }
        }


        public void ResetProgram()
        {
            this.ProgramRunning = false;

            if (this.Disk == null)
            {
                this.Program = null;
            }
            else
            {
                this.Program = this.Disk.codeHead;
            }

            if (this.DebugProgram && this.Program == null)
            {
                this.Program = ProgramHelper.DebugProgram();

                Debug.Log("Program Reset");
            }
        }


        private void ExecuteProgram()
        {
            if (this.ActionController.ActionCompleted())
            {
                CodeNode next = this.Program.GetNext(this.ActionController);

                if (next != null)
                {
                    this.Program = next;
                    this.Program.Execute(this.ActionController);
                }
                else
                {
                    if (this.Program.ContextNode != null)
                    {
                        this.Program = this.Program.ContextNode.AfterBreak();
                    }
                    else
                    {
                        this.ResetProgram();

                        if (this.DebugInfo)
                        {
                            Debug.Log("Program Ended");
                        }
                    }
                }
            }
            else
            {
                this.ActionController.Continue();
            }
        }

        #endregion


        #region === Animation Methods ===
        
        private void PrepareAnimationController()
        {
            this.AnimationController = this.transform.GetComponent<RobotAnimationController>();
        }
        
        #endregion

    }
}