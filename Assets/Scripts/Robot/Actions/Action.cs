using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Robot.Actions
{
    public class Action 
    {
        protected Transform Robot { get; set; }

        protected CodeNode ProgramLine { get; set; }



        public Action(Transform robot, CodeNode programLine)
        {
            this.Robot = robot;
            this.ProgramLine = programLine;
        }



        virtual public void Execute()
        {
            Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());

            // the line is done because the robot does not need to do nothing
            this.ProgramLine.Complete = true;
        }


        public bool Completed()
        {
            return this.ProgramLine.Complete;
        }
    }
}
