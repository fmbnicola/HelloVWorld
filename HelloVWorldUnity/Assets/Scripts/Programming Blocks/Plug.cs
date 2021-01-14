using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Threading;

namespace Block
{
    public class Plug : MonoBehaviour
    {
        [SerializeField]
        protected ProgrammingBlock Block;

        [SerializeField]
        private Socket Socket;


        // Interactable
        private XRGrabInteractable Grabbable;
        private LayerMask Mask;


        // Cable
        public Transform AnchorPoint;

        private ConnectorCable Cable;

        // Audio
        public AudioSource PlugAudioSource;
        public AudioSource UnPlugAudioSource;


        // State
        public enum States
        {
            OnAnchor,
            Grabbed,
            OnSocket
        }

        public States State { get; private set; }


        // Start is called before the first frame update
        void Start()
        {
            this.Cable = this.transform.Find("ConnectorCable").GetComponent<ConnectorCable>();

            this.Grabbable = this.GetComponent<OffsetGrab>();
            this.Mask = this.Grabbable.interactionLayerMask;

            this.State = States.OnAnchor;
        }


        // Update is called once per frame
        void LateUpdate()
        {
            // If Plug is Active
            if (this.Block.Active)
            {
                if (this.Grabbable == null) this.MakeGrabbable();

                switch (this.State)
                {
                    default:
                        break;

                    // And on its Anchor Point
                    case States.OnAnchor:
                        if (this.Grabbable.isSelected) this.State = States.Grabbed;
                        if (this.Socket != null) this.State = States.OnSocket;

                        // Keep it on the Anchor Point
                        this.transform.position = this.AnchorPoint.position;
                        this.transform.rotation = this.AnchorPoint.rotation;
                        break;

                    // And is on hand
                    case States.Grabbed:
                        // If it is let go
                        if (!this.Grabbable.isSelected)
                        {
                            this.State = States.OnAnchor;

                            // Return it the Anchor Point
                            this.transform.position = this.AnchorPoint.position;
                            this.transform.rotation = this.AnchorPoint.rotation;

                            // Reset the Cable
                            this.Cable.Clear();
                        }
                        break;

                    // And is on a Socket
                    case States.OnSocket:
                        // If it is Grabbed
                        if (this.Grabbable.isSelected && this.Socket == null)
                            this.State = States.Grabbed;
                        else
                        {
                            this.transform.position = this.Socket.transform.position;
                            this.transform.rotation = this.Socket.transform.rotation;
                        }
                        break;
                }
            }
            // If Plug is Inactive
            else
            {
                if(this.Grabbable != null) this.DestroyGrabbable();

                // Keep it on Anchor Point
                this.transform.position = this.AnchorPoint.position;
                this.transform.rotation = this.AnchorPoint.rotation;
            }
        }


        private void OnDestroy()
        {
            Destroy(this.Cable);
        }


        #region Getters
        public Socket GetConnectedTo()
        {
            return this.Socket;
        }

        public ProgrammingBlock GetBlock()
        {
            return this.Block;
        }
        #endregion


        #region Activation
        public void Activate()
        {
            this.MakeGrabbable();
        }

        public void Deactivate()
        {
            if (this.Socket != null) this.Eject();
            else this.DestroyGrabbable();

            this.Cable.Clear();

            this.State = States.OnAnchor;
        }
        #endregion


        #region Connect
        public void ConnectTo(Socket socket)
        {
            if (this.Socket != null || socket == null) return;

            this.ConnectedTo(socket);

            socket.ConnectedTo(this);
        }

        public void ConnectedTo(Socket socket)
        {
            this.Socket = socket;

            this.State = States.OnSocket;

            this.PlugAudioSource.Play();
        }
        #endregion


        #region Disconnect
        public void Disconnect()
        {
            var socket = this.Socket;

            if (socket == null) return;

            socket.Disconnected();

            this.Disconnected();
        }


        public void Disconnected()
        {
            this.Socket = null;

            if (this.Grabbable != null && this.Grabbable.isSelected) this.State = States.Grabbed;
            else
            {
                this.State = States.OnAnchor;

                this.transform.position = this.AnchorPoint.position;
                this.transform.rotation = this.AnchorPoint.rotation;
            }

            this.transform.SetParent(this.Block.transform);

            this.Cable.Clear();

            this.UnPlugAudioSource.Play();
        }
        #endregion


        #region Grabbable
        public void MakeGrabbable()
        {
            if (this.Grabbable != null) return;

            var grabbable = this.gameObject.AddComponent<OffsetGrab>();
            if (grabbable == null) grabbable = this.gameObject.GetComponent<OffsetGrab>();

            grabbable.interactionLayerMask = this.Mask;

            this.Grabbable = grabbable;
        }

        public void DestroyGrabbable()
        {
            if (this.Grabbable == null) return;

            Destroy(this.Grabbable);

            this.Grabbable = null;
        }
        #endregion


        public void Eject()
        {
            if(this.Socket != null)
            {
                this.DestroyGrabbable();

                this.Disconnect();
            }
        }
    }
}
