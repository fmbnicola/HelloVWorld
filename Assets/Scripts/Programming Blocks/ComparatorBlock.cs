using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using SudoProgram;

namespace Block
{
    public class ComparatorBlock : ConditionBlock
    {
        [SerializeField]
        private Comparator.ID Id = Comparator.ID.Equals;

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
                this.GetComponent<SphereCollider>().isTrigger = true;
            }

            if (!transform.GetComponent<XRGrabInteractable>().isSelected)
            {
                this.GetComponent<SphereCollider>().isTrigger = false;
            }
        }

        private void OnValidate()
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();

            propertyBlock.SetInt("_Comparator", (int)Id);

            var symbol = transform.Find("Symbol");
            var renderer = symbol.GetComponent<Renderer>();
            renderer.SetPropertyBlock(propertyBlock);
        }

        public Comparator Parse()
        {
            return new Comparator(this.Id);
        }

        public Comparator.ID GetId()
        {
            return this.Id;
        }

        public void Convert(Comparator.ID newType)
        {
            this.Id = newType;

            this.OnValidate();
        }
    }
}

