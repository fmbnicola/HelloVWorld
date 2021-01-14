using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorNode : MonoBehaviour
{
    public ConnectorCable Cable { get; private set; }

    public ConnectorNode Prev;
    public ConnectorNode Next;

    public int Index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }


    public void Initialize(ConnectorCable cable, int index)
    {
        this.Cable = cable;

        this.Index = index;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }
    */
}
