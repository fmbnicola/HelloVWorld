using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


namespace Block
{
    public class Socket : MonoBehaviour
    {
        [SerializeField]
        protected ProgrammingBlock Block;

        [SerializeField]
        protected Plug Plug;


        private SocketPlus SocketPlus;


        // Start is called before the first frame update
        void Start()
        {
            this.SocketPlus = this.GetComponent<SocketPlus>();

            this.SocketPlus.onSelectEnter.AddListener((interactable) => AttachPlug(interactable));
            this.SocketPlus.onSelectExit.AddListener((interactable) => DetachPlug(interactable));
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        public ProgrammingBlock GetBlock()
        {
            return this.Block;
        }


        #region Activation
        public void Activate()
        {
            this.InitiateGrabbing();
        }

        public void Deactivate()
        {
            if (this.Plug != null) this.Eject();
            else this.StopGrabbing();
        }
        #endregion


        #region Call Backs
        public void AttachPlug(XRBaseInteractable interactable)
        {
            var plug = interactable.GetComponent<Plug>();

            if (plug == null) return;

            this.ConnectTo(plug);
        }

        public void DetachPlug(XRBaseInteractable interactable)
        {
            var plug = interactable.GetComponent<Plug>();

            if (plug == null && plug == this.Plug) return;

            this.Disconnect();
        }
        #endregion


        #region Connected
        public void ConnectTo(Plug plug)
        {
            this.ConnectedTo(plug);

            plug.ConnectedTo(this);
        }


        public void ConnectedTo(Plug plug)
        {
            this.Plug = plug;
        }
        #endregion


        #region Disconnect
        public void Disconnect()
        {
            var plug = this.Plug;

            if (plug == null) return;

            plug.Disconnected();

            this.Disconnected();
        }


        public void Disconnected()
        {
            this.Plug = null;
        }
        #endregion


        #region Grabbing
        private void InitiateGrabbing()
        {
            this.SocketPlus.socketActive = true;
        }

        private void StopGrabbing()
        {
            this.SocketPlus.socketActive = false;
        }
        #endregion


        public void Eject()
        {
            if (this.Plug != null)
            {
                this.StopGrabbing();

                this.Disconnect();
            }
        }
    }
}