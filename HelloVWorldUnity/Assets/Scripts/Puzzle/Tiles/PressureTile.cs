using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Puzzle.Tiles
{
    public class PressureTile : MonoBehaviour
    {
        #region /* Set in Editor */

        public bool DebugInfo;

        public Material PressedMaterial;
        public Material UnpressedMaterial;

        #endregion

        #region /* Tile Info */
        
        private DetectionTile DetectTile { get; set; }
        
        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.DetectTile = this.transform.GetComponentInChildren<DetectionTile>();
        }


        // Update is called once per frame
        void Update()
        {

        }

        #endregion


        #region === Callback Function ===

        public void Pressed()
        {
            if (this.DebugInfo)
            {
                Debug.Log("Tile Pressed");
            }

            this.DetectTile.ChangeTileMaterial(this.PressedMaterial);
        }


        public void Unpressed()
        {
            if (this.DebugInfo)
            {
                Debug.Log("Tile No Longer Pressed");
            }

            this.DetectTile.ChangeTileMaterial(this.UnpressedMaterial);
        }

        #endregion
    }

}
