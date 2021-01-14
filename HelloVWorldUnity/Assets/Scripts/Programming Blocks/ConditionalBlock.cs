using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using SudoProgram;

namespace Block
{
    public abstract class ConditionalBlock : ProgrammingBlock
    {
        protected SensorBlock Sensor;
        protected ComparatorBlock Comparator;
        protected ValueBlock Value;

        public SocketPlus SensorSocket, ComparatorSocket, ValueSocket;

        protected Plug PlugTrue;
        protected Plug Plug;


        // Start is called before the first frame update
        void Start()
        {
            this.SensorSocket.onSelectEnter.AddListener((interactable) => RegisterSensor(interactable));
            this.SensorSocket.onSelectExit.AddListener((interactable) => DeregisterSensor(interactable));

            this.ComparatorSocket.onSelectEnter.AddListener((interactable) => RegisterComparator(interactable));
            this.ComparatorSocket.onSelectExit.AddListener((interactable) => DeregisterComparator(interactable));

            this.ValueSocket.onSelectEnter.AddListener((interactable) => RegisterValue(interactable));
            this.ValueSocket.onSelectExit.AddListener((interactable) => DeregisterValue(interactable));

            this.PlugTrue = this.transform.Find("PlugTrue").GetComponent<Plug>();
            this.Plug = this.transform.Find("Plug").GetComponent<Plug>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        #region Condition Blocks
        public void RegisterSensor(XRBaseInteractable interactable)
        {
            this.Sensor = interactable.GetComponent<SensorBlock>();
            Debug.Log(this.Sensor.GetId());
        }

        public void DeregisterSensor(XRBaseInteractable interactable)
        {
            var interactableSensor = interactable.GetComponent<SensorBlock>();

            if (interactableSensor.Equals(this.Sensor)) this.Sensor = null;

        }

        public void RegisterComparator(XRBaseInteractable interactable)
        {
            this.Comparator = interactable.GetComponent<ComparatorBlock>();
            Debug.Log(this.Comparator.GetId());
        }

        public void DeregisterComparator(XRBaseInteractable interactable)
        {
            var interactableComp = interactable.GetComponent<ComparatorBlock>();

            if (interactableComp.Equals(this.Comparator)) this.Comparator = null;
        }

        public void RegisterValue(XRBaseInteractable interactable)
        {
            this.Value = interactable.GetComponent<ValueBlock>();
            Debug.Log(this.Value.GetId());
        }

        public void DeregisterValue(XRBaseInteractable interactable)
        {
            var interactableVal = interactable.GetComponent<ValueBlock>();

            if (interactableVal.Equals(this.Value)) this.Value = null;
        }
        #endregion


        public override ProgrammingBlock GetNext()
        {
            if (this.Plug != null)
            {
                var connectedTo = this.Plug.GetConnectedTo();

                if (connectedTo != null)
                {
                    return connectedTo.GetBlock();
                }
            }

            return null;
        }

        public ProgrammingBlock GetNextIfTrue()
        {
            if (this.PlugTrue != null)
            {
                var connectedTo = this.PlugTrue.GetConnectedTo();

                if (connectedTo != null)
                {
                    return connectedTo.GetBlock();
                }
            }

            return null;
        }


        public CodeNode ParseInnerCode(CodeNode context)
        {
            var firstBlock = this.GetNextIfTrue();

            if (firstBlock == null)
            {
                return null;
            }

            var currentBlock = firstBlock;

            var firstNode = firstBlock.Parse(context, context);
            var currentNode = firstNode;

            currentBlock = currentBlock.GetNext();

            while (currentBlock != null)
            {
                var newNode = currentBlock.Parse(context, currentNode);

                currentNode.Next = newNode;
                currentNode = newNode;

                currentBlock = currentBlock.GetNext();
            }

            return firstNode;
        }


        public Condition ParseCondition()
        {
            return new Condition(this.Sensor.Parse(), this.Value.Parse(), this.Comparator.Parse());
        }


        public abstract override CodeNode Parse(CodeNode context, CodeNode prev);
    }
}
