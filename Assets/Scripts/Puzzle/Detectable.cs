﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Puzzle
{
    public class Detectable : MonoBehaviour
    {
        #region /* Attributes */

        public Value.ID Value;

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {

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
