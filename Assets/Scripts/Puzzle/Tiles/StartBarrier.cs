using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Puzzle.Tiles
{
    public class StartBarrier : MonoBehaviour
    {
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


        #region === Open/Close Methods ===
        
        public void Open()
        {
            this.gameObject.SetActive(false);
        }


        public void Close()
        {
            this.gameObject.SetActive(true);
        }

        #endregion
    }
}
