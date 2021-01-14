using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;

namespace Block
{
    public class ConditionBlock : ProgrammingBlock
    {
        // Start is called before the first frame update
        public bool Selected = false;
        public Vector3 SocketPos;
        public SocketPlus SocketConnected = null;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        protected void FixRotation()
        {
            if (this.Selected)
            {
                if (this.SocketConnected != null) transform.rotation = Quaternion.Euler(this.SocketConnected.transform.rotation.eulerAngles);

                else transform.rotation = Quaternion.Euler(this.SocketPos);
            }
        }
    }
}
