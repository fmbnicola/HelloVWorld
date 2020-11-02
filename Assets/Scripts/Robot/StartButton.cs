using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Robot
{
    public class StartButton : MonoBehaviour
    {
        private RobotController Robot { get; set; }

        public bool Clicked;



        // Start is called before the first frame update
        void Start()
        {
            this.Clicked = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (this.Clicked)
            {
                this.Clicked = false;
                this.Robot.StartProgram();
            }
        }


        
        public void Initialize(RobotController robot)
        {
            this.Robot = robot;
        }
    }
}