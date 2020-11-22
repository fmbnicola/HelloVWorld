using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Puzzle.Connectors
{
    public class OutputConnector : Connector
    {
        private ConnectorState prevState = ConnectorState.Off;
        private float prevPercent = 0.0f;

        [System.Serializable]
        public class ConnectEvent : UnityEvent { }

        public ConnectEvent OnTurnOn;
        public ConnectEvent OnTurnOff;

        public void Start()
        {
            //register output
            if (input != null)
            {
                input.RegisterOutput(this);
            }
        }

            //turn on and turn off connector from an input (like a button or pressure plate)
            public void Update()
        {
            if (input == null) return;

            var state = input.connectState;
            var percent = input.connectPercent;

            if (state == ConnectorState.On && percent == 1.0f)
            {
                this.connectState = ConnectorState.On;
                this.connectPercent = 1.0f;
            }
            else if (state == ConnectorState.Off)
            {
                this.connectState = ConnectorState.Off;
                this.connectPercent = 0.0f;
            }

            var stateChange = (prevState != state) || (prevPercent != percent);
            if (stateChange)
            {
                if(percent == 1.0f) OnTurnOn.Invoke();
                else OnTurnOff.Invoke();
            }

            prevPercent = percent;
            prevState = state;
        }
    }
}