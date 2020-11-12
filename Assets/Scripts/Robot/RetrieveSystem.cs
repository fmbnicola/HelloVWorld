using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot
{
    public class RetrieveSystem : MonoBehaviour
    {
        #region /* Editor Attributes */

        public RobotController Robot;

        public Transform DesiredPos;

        public Vector3 DesiredRot;

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            FixedButton button = this.GetComponentInChildren<FixedButton>();
            button.clickEvent.AddListener(() => this.Robot.SummonRobot(this.DesiredPos.position, this.DesiredRot));
        }


        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}
