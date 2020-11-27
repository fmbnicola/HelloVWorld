using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRPointerInteractable : MonoBehaviour
{

    // Event to be fired only once when clicked
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }
    public ButtonEvent ClickEvent;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        this.ClickEvent.Invoke();
    }

    public void Test()
    {
        Debug.Log("hello");
    }
}
