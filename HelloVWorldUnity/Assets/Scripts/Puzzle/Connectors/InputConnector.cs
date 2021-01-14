using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Connectors
{
    public class InputConnector : Connector
    {

        //turn on and turn off connector from an input (like a button or pressure plate)
        public void TurnOn()
        {
            this.connectState = ConnectorState.On;
            this.connectPercent = 1.0f;
        }

        public void TurnOff()
        {
            this.connectState = ConnectorState.Off;
            this.connectPercent = 0.0f;
        }
    }
}
