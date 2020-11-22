using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Connectors
{
    public class LogicalConnector : Connector
    {
        private Renderer connectRenderer;
        private MaterialPropertyBlock propertyBlock;

        [SerializeField]
        public List<Connector> inputs = new List<Connector>();

        public enum ConnectorLogicType { AND, OR}
        public ConnectorLogicType logicType = ConnectorLogicType.AND;

        // Start is called before the first frame update
        public void Start()
        {
            connectRenderer = this.GetComponent<Renderer>();

            //register output in all inputs
            if(inputs.Count != 0){
                foreach(Connector _input in inputs)
                {
                    _input.RegisterOutput(this);
                }
            }   
        }

        // Update is called once per frame
        public void Update()
        {
            UpdateState();
            UpdateLook();
        }

        protected void UpdateState()
        {
            var combinedVal = false;
            if (inputs.Count != 0)
            {
                switch (logicType)
                {
                    case ConnectorLogicType.AND:

                        combinedVal = true;
                        foreach (Connector _input in inputs)
                        {
                            var val = ( _input.connectState == ConnectorState.On && 
                                        _input.connectPercent == 1.0f);
                            combinedVal = combinedVal && val;
                        }
                        break;

                    case ConnectorLogicType.OR:

                        combinedVal = false;
                        foreach (Connector _input in inputs)
                        {
                            var val = ( _input.connectState == ConnectorState.On &&
                                        _input.connectPercent == 1.0f);
                            combinedVal = combinedVal || val;
                        }
                        break;
                }
            }
            this.connectState = combinedVal? ConnectorState.On : ConnectorState.Off;
            this.connectPercent = (this.connectState == ConnectorState.On) ? 1.0f: 0.0f;
        }

        protected void UpdateLook()
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();

            propertyBlock.SetFloat("_Percentage", this.connectPercent);
            connectRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}