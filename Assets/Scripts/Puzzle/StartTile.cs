using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot;



namespace Puzzle
{
    public class StartTile : MonoBehaviour
    {
        #region /* Set in Editor */

        public Vector3 StartRotation;

        public bool DebugInfo;

        #endregion


        #region /* Detect Info */
        
        private DetectionTile DetectTile { get; set; }
        private RobotController Robot { get; set; }
        
        private Vector3 StartPos { get; set; }
        
        #endregion


        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.DetectTile = this.transform.GetComponentInChildren<DetectionTile>();

            this.StartPos = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion


        #region === Callback Functions ===
        
        public void RobotAtStart()
        {
            GameObject detected = this.DetectTile.ObjectDetected;
            this.Robot = detected.GetComponent<RobotController>();

            if (this.StartPos == Vector3.zero)
            {
                this.DefineStartPosition();
            }

            this.StartExecution();

            if (this.DebugInfo)
            {
                Debug.Log("Robot in start position");
            }
        }


        public void LeaveStart()
        {
            this.Robot.InStartPosition = false;
            this.Robot = null;

            if (this.DebugInfo)
            {
                Debug.Log("Robot left start position");
            }
        }

        #endregion


        #region === Auxiliar Functions ===

        private void DefineStartPosition()
        {
            Vector3 detectCenter = this.transform.position;

            this.StartPos = new Vector3(detectCenter.x, this.Robot.Height, detectCenter.z);
        }


        private void StartExecution()
        {
            this.Robot.SummonRobot(this.StartPos, this.StartRotation);
            this.Robot.InStartPosition = true;
            this.Robot.StartProgram();
        }

        #endregion
    }
}
