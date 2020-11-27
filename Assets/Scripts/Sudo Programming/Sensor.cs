using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Robot.Actions;


using Block;


namespace SudoProgram
{
    public class Sensor
    {
        public enum ID
        {
            Camera,
            Microphone
        }

        public ID Id { get; protected set; }


        public Sensor(ID id)
        {
            this.Id = id;
        }


        public Value GetValue(ActionController robot)
        {
            return robot.Sense(this.Id);
        }
    }
}
