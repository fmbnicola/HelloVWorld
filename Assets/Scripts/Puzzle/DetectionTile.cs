using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace Puzzle
{
    public class DetectionTile : MonoBehaviour
    {
        #region /* Set in Editor */

        [SerializeField]
        public DetectableTags DesiredTag;

        #endregion


        #region /* Detection Attributes */
        
        #region Detect Events
        [System.Serializable]
        public class DetectEvent : UnityEvent { }
        
        public DetectEvent OnDetect;
        public DetectEvent OnDetectEnd;
        #endregion

        public enum DetectableTags { Robot }

        public GameObject ObjectDetected { get; private set; }

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


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(this.DesiredTag.ToString()))
            {
                this.ObjectDetected = other.gameObject;
                this.OnDetect.Invoke();

                Debug.Log("Tile Detected a " + this.DesiredTag);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(this.DesiredTag.ToString()))
            {
                this.ObjectDetected = null;
                this.OnDetectEnd.Invoke();

                Debug.Log(this.DesiredTag + " no longer on detection tile");
            }
        }

        #endregion
    }
}
