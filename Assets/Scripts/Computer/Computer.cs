using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public ProgrammingBlock StartBlock;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public CodeNode Parse()
    {
        if(this.StartBlock != null)
        {
            CodeNode head = null;
            CodeNode prev = null;

            var block = this.StartBlock.GetNext();

            while(block != null)
            {
               
                var node = block.Parse(null, prev);

                if (head == null) head = node;
                else
                {
                    prev.Next = node;

                }

                prev = node;

                block = block.GetNext();
            }

            if (this.transform.Find("FloppyDisk") != null)
            {
                this.transform.Find("FloppyDisk").gameObject.GetComponent<FloppyDisk>().codeHead = head;
               
            }
            Debug.Log(this.transform.Find("FloppyDisk").gameObject.GetComponent<FloppyDisk>().codeHead);
            return head;
        }

        return null;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "FloppyDisk")
        {
            //ver com nico como se deteta que ela esta a ser agarrada

            collider.gameObject.transform.parent = this.transform;
            collider.gameObject.transform.localPosition = new Vector3(-0.277299f, 0.453f, 0);

            Vector3 rotationVector = new Vector3(0 - 0.089f, 179.991f, -4.008f);
            Quaternion rotation = Quaternion.Euler(rotationVector);
            collider.gameObject.transform.localRotation = rotation;
            
            collider.gameObject.GetComponent<FloppyDisk>().inserted = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        collider.gameObject.GetComponent<FloppyDisk>().inserted = false;
        collider.gameObject.transform.parent = null;
        //if (this.Computer.StartBlock != null && collider.gameObject == this.Computer.StartBlock)
        //  this.Computer.StartBlock = null;
    }
}
