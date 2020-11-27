using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class VRInputModule : MonoBehaviour
{
    public InputHelpers.Button Button;
    public ControllerInput Controller;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckClicked();
    }


    private void CheckClicked()
    {
        if ( this.Controller.GetButtonDown(this.Button) || Input.GetKeyDown(KeyCode.Q) )
        {
            if ( this.Target != null )
            {
                var interactable = this.Target.GetComponent<VRPointerInteractable>();

                if (interactable != null) interactable.Click();
            }
        }
    }
}
