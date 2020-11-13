using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Puzzle
{
    public class StartTile : MonoBehaviour
    {
        #region /* Set in Editor */

        public Vector3 StartRotation;
        
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

        #endregion


        #region === Callback Functions ===
        
        public void RobotAtStart()
        {

            Debug.Log("Robot in start position");
        }
        
        #endregion
    }
}
