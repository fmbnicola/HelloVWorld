﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace Puzzle
{
    public class DetectionTile : MonoBehaviour
    {
        #region /* Set in Editor */

        public DetectableTags DesiredTag;

        public bool DebugInfo;

        #endregion


        #region /* Detection Attributes */
        
        #region Detect Events
        [System.Serializable]
        public class DetectEvent : UnityEvent { }
        
        public DetectEvent OnDetect;
        public DetectEvent OnDetectEnd;
        #endregion

        public enum DetectableTags { Everything,
                                     Untagged,
                                     Robot      }

        public GameObject ObjectDetected { get; private set; }

        public BoxCollider DetectBox { get; private set; }

        #endregion


        #region /* Tile Attributes */
        private Renderer TileRenderer { get; set; }

        #endregion



        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            this.DetectBox = this.transform.GetComponent<BoxCollider>();

            this.TileRenderer = this.transform.GetComponentInChildren<MeshRenderer>();
        }


        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerEnter(Collider other)
        {
            if (this.DesiredTag == DetectableTags.Everything ||
                other.CompareTag(this.DesiredTag.ToString()))
            {
                this.ObjectDetected = other.gameObject;
                this.OnDetect.Invoke();

                if (this.DebugInfo)
                {
                    Debug.Log(other.gameObject.name + " Detected");
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (this.DesiredTag == DetectableTags.Everything ||
                other.CompareTag(this.DesiredTag.ToString()))
            {
                this.ObjectDetected = null;
                this.OnDetectEnd.Invoke();

                if (this.DebugInfo)
                {
                    Debug.Log(other.gameObject.name + "No Longer Detected");
                }
            }
        }

        #endregion


        #region === Tile Methods ===

        public void ChangeTileMaterial(Material newMat)
        {
            Material[] mats = this.TileRenderer.materials;

            mats[0] = newMat;

            this.TileRenderer.materials = mats;
        }

        #endregion
    }
}
