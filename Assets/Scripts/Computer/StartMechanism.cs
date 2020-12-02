using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot;
using SudoProgram;



namespace ComputerElements
{
    public class StartMechanism : MonoBehaviour
    {
        #region /* Robot Attributes */

        public RobotController Robot;

        public Vector3 DesiredRot;
        private Vector3 DesiredPos { get; set; }

        private bool RobotLoaded { get; set; }

        #endregion


        #region /* Effect Attributes */
        
        private SpawnEffect Effect { get; set; }

        private bool RobotFullyInScene { get; set; }

        #endregion



        #region === Unity Event ===

        // Start is called before the first frame update
        void Start()
        {
            this.DefineDesiredPos();

            this.InitializeEffect();
        }


        // Update is called once per frame
        void Update()
        {
            // Robot ready to start
            if (!this.RobotFullyInScene && !this.Effect.executing)
            {
                this.RobotFullyInScene = true;
            }
        }

        #endregion


        #region === Robot Methods ===

        private void DefineDesiredPos()
        {
            Transform target = this.transform.Find("Target");
            Vector3 targetPos = target.position;

            this.DesiredPos = new Vector3(targetPos.x, targetPos.y + this.Robot.Height, targetPos.z);
        }

        #endregion


        #region === Effect Methods ===

        private void InitializeEffect()
        {
            this.Effect = this.transform.GetComponent<SpawnEffect>();
            this.Effect.Initialize(this.Robot.gameObject);

            this.RobotFullyInScene = false;
            this.Robot.Active(false);
        }

        #endregion


        #region === Execution Methods ===

        public void MakeRobotAppear()
        {
            this.Robot.Active(true);

            this.Robot.SummonRobot(this.DesiredPos, this.DesiredRot);
            this.Effect.Execute();

            this.RobotLoaded = false;
        }


        public void LoadProgram(CodeNode program)
        {
            // TODO: if robot active then make him explode

            if (this.RobotLoaded)
            {
                this.MakeRobotAppear();
            }

            this.Robot.LoadProgram(program);
            this.RobotLoaded = true;
        }


        public void MakeRobotDesapear() 
        {
            // TODO: explosion effect

            this.Robot.Active(false);
            this.RobotFullyInScene = false;
        }
        
        #endregion
    }
}
