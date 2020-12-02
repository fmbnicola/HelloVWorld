using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;



namespace Puzzle
{
    public class Detectable : MonoBehaviour
    {
        #region /* Attributes */

        public Value.ID Value;

        public bool Active { get; set; }

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.Active = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion



        #region === Value Info ===

        public Value GetValue()
        {
            return new Value(this.Value);
        }

        #endregion
    }
}
