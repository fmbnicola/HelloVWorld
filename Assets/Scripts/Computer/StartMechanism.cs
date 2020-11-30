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

        #endregion


        #region /* Effect Attributes */
        
        private SpawnEffect Effect { get; set; }

        #endregion


        #region /* Program Attributes */

        private CodeNode Program { get; set; }
        
        #endregion



        #region === Unity Event ===

        // Start is called before the first frame update
        void Start()
        {
            this.DefineDesiredPos();

            this.InitializeEffect();

            this.Robot.Active(false);
        }


        // Update is called once per frame
        void Update()
        {
            if (this.Program != null && !this.Effect.executing)
            {
                this.Robot.LoadProgram(this.Program);
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
        }

        #endregion


        #region === Execution Methods ===
        
        public void StartProgram(CodeNode program)
        {
            this.Program = program;

            this.Robot.Active(true);

            this.Robot.SummonRobot(this.DesiredPos, this.DesiredRot);
            this.Effect.Execute();
        }
        
        #endregion
    }
}
