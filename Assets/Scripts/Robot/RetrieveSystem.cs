using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Robot
{
    public class RetrieveSystem : MonoBehaviour
    {
        #region /* Editor Attributes */

        public RobotController Robot;

        public Transform DesiredPos;

        public Vector3 DesiredRot;

        public Material TeleportMaterial;
        public GameObject ParticleSystem;
        #endregion

        private SpawnEffect Effect;


        #region === Unity Events ===

        // Start is called before the first frame update
        void Start()
        {
            // Handle spawn effect
            Effect = gameObject.AddComponent<SpawnEffect>();
            Effect.Initialize(this.Robot.gameObject, this.TeleportMaterial, this.ParticleSystem);

            FixedButton button = this.GetComponentInChildren<FixedButton>();
            button.clickEvent.AddListener(() => this.CallbackFunc(this.DesiredPos.position, this.DesiredRot));
        }
        
        void CallbackFunc(Vector3 pos, Vector3 rot)
        {
            this.Robot.SummonRobot(pos, rot);
            this.Effect.Execute();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}
