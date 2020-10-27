using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dias : MonoBehaviour
{
    private Computer Computer;


    // Start is called before the first frame update
    void Start()
    {
        this.Computer = this.transform.parent.GetComponent<Computer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "StartBlock")
        {
            this.Computer.StartBlock = collider.gameObject.GetComponent<ProgrammingBlock>();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (this.Computer.StartBlock != null && collider.gameObject == this.Computer.StartBlock)
            this.Computer.StartBlock = null;
    }
}
