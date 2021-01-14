using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;

namespace Block
{
    public class ProgrammingBlock : MonoBehaviour
    {
        [SerializeField]
        protected List<Plug> Plugs;

        [SerializeField]
        protected Socket Socket;


        protected MaterialPropertyBlock propertyBlock;
        public Renderer BlockRenderer;


        [SerializeField]
        public bool Active;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnDestroy()
        {
            foreach (var plug in this.Plugs)
                Destroy(plug);
        }


        #region Parsing
        public virtual ProgrammingBlock GetNext()
        {
            if (this.Plugs == null || this.Plugs.Count == 0) return null;

            var plug = this.Plugs[0];

            if (plug != null)
            {
                var connectedTo = plug.GetConnectedTo();

                if (connectedTo != null)
                {
                    return connectedTo.GetBlock();
                }
            }

            return null;
        }


        public virtual CodeNode Parse(CodeNode context, CodeNode prev)
        {
            return null;
        }
        #endregion


        #region Activation
        public void Activate()
        {
            if (this.Active) return; 
                
            this.Active = true;

            foreach(var plug in this.Plugs)
            {
                plug.Activate();
            }

            if (this.Socket != null) this.Socket.Activate();

            Debug.Log("Activated - " + this.name);
        }


        public void Deactivate()
        {
            if (!this.Active) return;
            
            this.Active = false;

            foreach(var plug in this.Plugs)
            {
                plug.Deactivate();
            }

            if (this.Socket != null) this.Socket.Deactivate();

            Debug.Log("Deactivated - " + this.name);
        }
        #endregion


        #region Highlight
        public virtual void Highlight()
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetFloat("_OutlineWidth", 0.1f);
        BlockRenderer.SetPropertyBlock(propertyBlock);

            Debug.Log(this.ToString() + " highlighted");
        }

        public virtual void UnHighlight()
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetFloat("_OutlineWidth", 0.0f);
        BlockRenderer.SetPropertyBlock(propertyBlock);

            Debug.Log(this.ToString() + " remove highlight");
        }
        #endregion
    }
}
