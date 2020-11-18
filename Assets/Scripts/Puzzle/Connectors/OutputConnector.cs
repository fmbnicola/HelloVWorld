using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Puzzle.Connectors
{
    public class OutputConnector : Connector
    {
        ConnectorState prevState = ConnectorState.Off;

        [System.Serializable]
        public class ConnectEvent : UnityEvent { }

        public ConnectEvent OnTurnOn;
        public ConnectEvent OnTurnOff;

        //turn on and turn off connector from an input (like a button or pressure plate)
        public void Update()
        {
            if (input == null) return;

            var state = input.connectState;
            if (prevState != state)
            {
                if (state == ConnectorState.On) OnTurnOn.Invoke();
                if (state == ConnectorState.Off) OnTurnOff.Invoke();
            }
            prevState = state;
        }
    }
}