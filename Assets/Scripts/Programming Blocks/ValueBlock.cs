using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ValueBlock : ConditionBlock
{
    [SerializeField]
    private Value.ID Id = Value.ID.Empty;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.FixRotation();

        if (transform.GetComponent<XRGrabInteractable>().isSelected && !this.Selected)
        {
            this.GetComponent<BoxCollider>().isTrigger = true;
        }

        if (!transform.GetComponent<XRGrabInteractable>().isSelected)
        {
            this.GetComponent<BoxCollider>().isTrigger = false;
        }

    }


    public Value Parse()
    {
        return new Value(this.Id);
    }

    public Value.ID GetId()
    {
        return this.Id;
    }
}
