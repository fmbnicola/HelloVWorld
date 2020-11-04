using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorNode : MonoBehaviour
{
    public ConnectorNode Prev;
    public ConnectorNode Next;

    public int Index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1);
    }
}
