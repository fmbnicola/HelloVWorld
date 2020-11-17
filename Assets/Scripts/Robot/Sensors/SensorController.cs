using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Puzzle;



namespace Robot.Sensors
{
    public class SensorController : MonoBehaviour
    {
        #region /* Sensors */

        private RobotCamera Camera { get; set; }

        #endregion

        

        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.Camera = this.transform.GetComponentInChildren<RobotCamera>();
        }


        // Update is called once per frame
        void Update()
        {

        }

        #endregion


        #region === Sensors Methods ===

        public Value Sense(Sensor.ID sensor)
        {
            switch (sensor)
            {
                case Sensor.ID.Camera:
                    return this.Camera.GetValue();

                default:
                    return new Value(Value.ID.Empty);
            }
        }

        #endregion
    }
}
