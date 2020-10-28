using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot.Actions
{
    public class ActionController : MonoBehaviour
    {
        private Action CurrentAction { get; set; }




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



        #region === Action Methods ===

        public void Execute(CodeNode programLine)
        {
            this.CurrentAction = new Action(this.transform, programLine);

            bool complete = this.CurrentAction.Execute();

            if (complete)
            {
                this.CurrentAction = null;
            }
        }


        public bool CurrentActionCompleted()
        {
            if (this.CurrentAction != null)
            {
                return this.CurrentAction.Completed();
            }

            return true;
        }

        #endregion
    }
}
