using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using SudoProgram;

namespace Block
{
    public class ValueBlock : ConditionBlock
    {
        [SerializeField]
        private Value.ID Id = Value.ID.Empty;

        private MaterialPropertyBlock propertyBlock;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            base.FixRotation();

            if (transform.GetComponent<XRGrabInteractable>().isSelected && !this.Selected)
            {
                this.GetComponent<BoxCollider>().isTrigger = true;
            }

            if (!transform.GetComponent<XRGrabInteractable>().isSelected)
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }

        }

        private void OnValidate()
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();

            propertyBlock.SetInt("_Value", (int)Id);

            var symbol = transform.Find("Symbol");
            var renderer = symbol.GetComponent<Renderer>();
            renderer.SetPropertyBlock(propertyBlock);
        }

        public Value Parse()
        {
            return new Value(this.Id);
        }

        public Value.ID GetId()
        {
            return this.Id;
        }

        public void Convert(Value.ID newType)
        {
            this.Id = newType;

            this.OnValidate();
        }
    }
}
