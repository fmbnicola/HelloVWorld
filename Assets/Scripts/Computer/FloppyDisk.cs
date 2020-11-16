using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyDisk : MonoBehaviour
{

    public bool inserted = false;

    public CodeNode codeHead = null;
    public bool Selected = false;
    public Vector3 SocketPos;
    public SocketPlus SocketConnected = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Selected)
        {
            if (this.SocketConnected != null) transform.rotation = Quaternion.Euler(this.SocketConnected.transform.rotation.eulerAngles);

            else transform.rotation = Quaternion.Euler(this.SocketPos);
        }
    }
}
