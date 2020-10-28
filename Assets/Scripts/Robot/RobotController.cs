using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        // TODO: change to correct type
        private List<Object> Program { get; set; }



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



        #region === Program Methods ===

        public void LoadProgram(List<Object> newProgram)
        {
            this.Program = newProgram;
        }


        public void ExecuteProgram()
        {
            foreach(Object instruction in this.Program)
            {
                // this.ActionController.Execute(instruction);
            }
        }

        #endregion

    }
}