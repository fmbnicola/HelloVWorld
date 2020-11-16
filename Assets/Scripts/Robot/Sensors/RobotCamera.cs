using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Puzzle;



namespace Robot.Sensors
{
    public class RobotCamera : MonoBehaviour
    {
        #region /* Object 'Seen' */
        
        private Detectable Obstacule { get; set; }

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerStay(Collider other)
        {
            this.Obstacule = other.transform.GetComponent<Detectable>();
        }


        private void OnTriggerExit(Collider other)
        {
            if (this.Obstacule != null && other.gameObject == this.Obstacule.gameObject)
            {
                this.Obstacule = null;
            }
        }

        #endregion


        #region === Obstacule Info ===

        public Value GetValue()
        {
            if (this.Obstacule == null)
            {
                return new Value(Value.ID.Empty);
            }

            return this.Obstacule.GetValue();
        }

        #endregion
    }
}
