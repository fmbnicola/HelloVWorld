using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Robot.Actions
{
    public class Action 
    {
        private Transform Robot { get; set; }

        private CodeNode ProgramLine { get; set; }



        public Action(Transform robot, CodeNode programLine)
        {
            this.Robot = robot;
            this.ProgramLine = programLine;
        }



        public bool Execute()
        {
            Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());

            // the line is done because the robot does not need to do nothing
            this.ProgramLine.Complete = true;

            return this.ProgramLine.Complete;
        }


        public bool Completed()
        {
            return this.ProgramLine.Complete;
        }
    }
}
