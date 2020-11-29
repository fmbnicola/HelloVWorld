using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRetriever : MonoBehaviour
{
    public BlockManager Manager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;

        if(obj.CompareTag("Block"))
        {
            this.Manager.DeSpawn(obj);
        }
    }
}
