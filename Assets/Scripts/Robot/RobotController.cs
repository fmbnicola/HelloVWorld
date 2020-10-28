using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        private ActionController ActionController { get; set; }


        private CodeNode Program { get; set; }



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.ActionController = this.transform.GetComponent<ActionController>();

            this.Program = null;
        }


        // Update is called once per frame
        void Update()
        {
            //this.ExecuteProgram();
        }


        private void OnTriggerStay(Collider other)
        {
            if (this.Program == null && other.CompareTag("Disk"))
            {
                this.LoadProgram(other.gameObject);
            }
        }

        #endregion



        #region === Program Methods ===

        public void LoadProgram(GameObject disk)
        {
            // get program head from floppy disk
            this.Program = new CodeNode(null, null);
            Debug.Log("Program loaded");
        }


        private void ExecuteProgram()
        {
            CodeNode instruction = this.Program;
        }

        #endregion

    }
}