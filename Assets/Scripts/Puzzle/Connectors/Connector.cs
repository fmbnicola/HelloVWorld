using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Connectors
{
    public class Connector : MonoBehaviour
    {
        public Connector input = null;
        private List<Connector> outputs = new List<Connector>();

        public enum ConnectorState { On, Off };
        public ConnectorState connectState = ConnectorState.Off;
        public float connectPercent = 0.0f;

        public void RegisterOutput(Connector output)
        {
            this.outputs.Add(output);
        }
    }
}

