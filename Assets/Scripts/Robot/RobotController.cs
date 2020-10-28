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
        }


        // Update is called once per frame
        void Update()
        {
            this.ExecuteProgram();
        }

        #endregion



        #region === Program Methods ===

        public void LoadProgram(CodeNode programHead)
        {
            this.Program = programHead;
        }


        private void ExecuteProgram()
        {
            CodeNode instruction = this.Program;


        }

        #endregion

    }
}