﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;


namespace Robot.Actions
{
    public class Action 
    {
        protected RobotController Robot { get; set; }
        protected RobotAnimationController AnimationController { get; set; }

        protected CodeNode ProgramLine { get; set; }



        public Action(RobotController robot, CodeNode programLine)
        {
            this.Robot = robot;
            this.AnimationController = this.Robot.AnimationController;

            this.ProgramLine = programLine;
        }



        virtual public void Execute()
        {
            if (this.Robot.DebugInfo)
            {
                Debug.Log(this.ProgramLine.ToString() + " -> " + this.ToString());
            }

            this.ProgramLine.Complete = true;
        }


        virtual public bool Completed()
        {
            return this.ProgramLine.Complete;
        }


        virtual public void Terminate() { }
    }
}
