using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammingBlock : MonoBehaviour
{
    protected Transform Port;

    protected float track;

    [SerializeField]
    float id;

    // Start is called before the first frame update
    void Start()
    {
        this.track = 0;

        this.Port = CheckPort();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected Transform CheckPort()
    {
        return this.transform.Find("Port");
    }
}
