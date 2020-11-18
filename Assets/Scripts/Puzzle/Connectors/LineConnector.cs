using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Connectors
{
    public class LineConnector : Connector
    {
        private Renderer connectRenderer;
        private MaterialPropertyBlock propertyBlock;

        // Start is called before the first frame update
        public void Start()
        {
            connectRenderer = this.GetComponent<Renderer>();

            //register output
            if(input != null){
                input.RegisterOutput(this);
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
            if (input != null)
            {
                this.connectState = input.connectState;
            }

            this.connectPercent = (connectState == ConnectorState.On) ? 1.0f: 0.0f;
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