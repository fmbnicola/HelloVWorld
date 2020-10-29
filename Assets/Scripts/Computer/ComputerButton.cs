using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerButton : MonoBehaviour
{
    [SerializeField]
    private bool Clicked = false;

    [SerializeField]
    private Computer Computer;

    public enum Types
    {
        StartUp,
        Save,
        ShutDown,
        Clear
    }

    public Types Type = Types.Save;

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

            switch(this.Type)
            {
                default:
                    break;

                case Types.StartUp:
                    this.Computer.StartUp();
                    break;

                case Types.Save:
                    this.Computer.Save();
                    break;

                case Types.Clear:
                    break;

                case Types.ShutDown:
                    this.Computer.ShutDown();
                    break;
            }
        }
    }
}
