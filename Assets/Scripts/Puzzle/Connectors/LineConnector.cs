using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Connectors
{
    public class LineConnector : Connector
    {
        private Renderer connectRenderer;
        private MaterialPropertyBlock propertyBlock;

        private float targetPerccent = 0.0f;

        private float speed = 5.0f;
        private float speedFactor = 1.0f;

        // Start is called before the first frame update
        public void Start()
        {
            connectRenderer = this.GetComponent<Renderer>();

            //register output
            if(input != null){
                input.RegisterOutput(this);
            }

            //speed is modulated by the x scale (because of diferent cable lengths)
            speedFactor = speed / transform.localScale.x;
        }

        // Update is called once per frame
        public void Update()
        {
            UpdateState();
            UpdateLook();
        }

        protected float ConstantInterpolation(float val, float target, float t)
        {
            var sign = Mathf.Sign(target - val);
            var newval = val + (sign * t);

            if (sign > 0)
                return Mathf.Min(newval, target);
            else
                return Mathf.Max(newval, target);
        }

        protected void UpdateState()
        {
            if (input != null)
            {
                // Propagate on/off state
                this.connectState = input.connectState;

                // Evaluate input and find if wire should power on
                var canPowerOn = (this.connectState == ConnectorState.On);
                canPowerOn &= (input.connectPercent == 1.0f);

                if (canPowerOn) targetPerccent = 1.0f;

                if(transform.name == "LineConnector (4)")
                {
                    Debug.Log("heyo");
                }

                // Evaluate outputs to find if wire should power off
                if (outputs.Count != 0) {

                    var canPowerOff = (this.connectState == ConnectorState.Off);
                    foreach(var output in this.outputs)
                    {
                        canPowerOff &= (output.connectPercent == 0.0f);
                    }
                    if (canPowerOff) targetPerccent = 0.0f;
                }
            }
           
            connectPercent = ConstantInterpolation(connectPercent, targetPerccent, Time.deltaTime * speedFactor);
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