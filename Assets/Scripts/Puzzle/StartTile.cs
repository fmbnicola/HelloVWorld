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


        #region /* Start Postion */

        private float X { get; set; }
        private float Z { get; set; }

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            BoxCollider trigger = this.GetComponent<BoxCollider>();

            this.X = trigger.center.x;
            this.Z = trigger.center.z;
        }


        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(this.RobotTag))
            {
                //other.transform.rotation = Quaternion.Euler(this.Rotation);
                //other.transform.position = new Vector3(this.X, other.transform.position.y, this.Z);
            }
        }

        #endregion
    }
}
