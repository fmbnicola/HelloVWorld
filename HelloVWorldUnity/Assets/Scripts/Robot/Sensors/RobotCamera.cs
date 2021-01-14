using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Puzzle;

using SudoProgram;

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
            var detectable = other.transform.GetComponent<Detectable>();

            if (detectable != null)
            {
                if (this.Obstacule == null || 
                   (this.Obstacule.gameObject != other.gameObject))
                {
                    this.Obstacule = detectable;
                }

                Debug.Log("Detected: " + other.ToString());
            }
        }


        private void OnTriggerExit(Collider other)
        {
            var detectable = other.transform.GetComponent<Detectable>();

            if (detectable != null)
            {
                if (this.Obstacule != null && this.Obstacule.gameObject == other.gameObject)
                {
                    this.Obstacule = null;
                }

                Debug.Log("No Longer Detected: " + other.ToString());
            }
        }

        #endregion


        #region === Obstacule Info ===

        public Value GetValue()
        {
            if (this.Obstacule != null && !this.Obstacule.Active)
            {
                this.Obstacule = null;
            }

            if (this.Obstacule == null)
            {
                return new Value(Value.ID.Empty);
            }

            return this.Obstacule.GetValue();
        }

        #endregion
    }
}
