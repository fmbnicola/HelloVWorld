using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot;



namespace Puzzle.Tiles
{
    public class StartTile : MonoBehaviour
    {
        #region /* Set in Editor */

        public bool DebugInfo;

        #endregion


        #region /* Detect Info */
        
        private DetectionTile DetectTile { get; set; }
        private RobotController Robot { get; set; }
        
        private Vector3 StartPos { get; set; }

        #endregion


        #region /* Barrier Info */
        
        private StartBarrier Barrier { get; set; }

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.DetectTile = this.transform.GetComponentInChildren<DetectionTile>();

            this.Barrier = this.transform.GetComponentInChildren<StartBarrier>();

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
            if (this.Robot == null)
            {
                GameObject detected = this.DetectTile.ObjectDetected;
                this.Robot = detected.GetComponentInParent<RobotController>();
            }

            if (this.StartPos == Vector3.zero)
            {
                this.DefineStartPosition();
            }

            this.Barrier.Open();
            this.Robot.AtStartPosition(this.StartPos, this.transform.rotation.eulerAngles);

            if (this.DebugInfo)
            {
                Debug.Log("Robot in start position");
            }
        }


        public void LeaveStart()
        {
            this.Robot.LeaveStartPosition();
            this.Robot = null;

            this.Barrier.Close();

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

        #endregion
    
    }
}
