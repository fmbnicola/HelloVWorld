using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;



namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        private ActionController ActionController { get; set; }


        // TODO: change to correct type
        private List<CodeNode> Program { get; set; }



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.ActionController = this.transform.GetComponent<ActionController>();
        }


        // Update is called once per frame
        void Update()
        {

        }

        #endregion



        #region === Program Methods ===

        public void LoadProgram(List<CodeNode> newProgram)
        {
            this.Program = newProgram;
        }


        public void ExecuteProgram()
        {
            foreach(CodeNode instruction in this.Program)
            {
                //instruction.Execute(this.ActionController);
                //this.ActionController.Execute(instruction);
            }
        }

        #endregion

    }
}