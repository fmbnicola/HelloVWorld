using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Puzzle
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

        private Renderer TileRenderer { get; set; }
        
        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.DetectTile = this.transform.GetComponentInChildren<DetectionTile>();

           this.TileRenderer = this.transform.GetComponentInChildren<MeshRenderer>();
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

            this.ChangeTileMaterial(this.PressedMaterial);
        }


        public void Unpressed()
        {
            if (this.DebugInfo)
            {
                Debug.Log("Tile No Longer Pressed");
            }

            this.ChangeTileMaterial(this.UnpressedMaterial);
        }

        #endregion


        #region === Tile Functions ===

        private void ChangeTileMaterial(Material newMat)
        {
            Material[] mats = this.TileRenderer.materials;

            mats[0] = newMat;

            this.TileRenderer.materials = mats;
        }
        
        #endregion
    }

}
