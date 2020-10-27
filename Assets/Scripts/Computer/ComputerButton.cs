using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerButton : MonoBehaviour
{
    [SerializeField]
    private bool Clicked = false;

    [SerializeField]
    private Computer Computer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.Clicked)
        {
            this.Clicked = false;

            this.Computer.Parse();
        }
    }
}
