using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Puzzle
{
    public class StartTile : MonoBehaviour
    {
        #region /* Set in Editor */

        public Vector3 Rotation;

        public string RobotTag;

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
            if (other.CompareTag(this.RobotTag))
            {
                Debug.Log("robot detected");
            }
        }

        #endregion
    }
}
