using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ComparatorBlock : ConditionBlock
{
    [SerializeField]
    private Comparator.ID Id = Comparator.ID.Equals;


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
            this.GetComponent<SphereCollider>().isTrigger = true;
        }

        if (!transform.GetComponent<XRGrabInteractable>().isSelected)
        {
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
    }


    public Comparator Parse()
    {
        return new Comparator(this.Id);
    }

    public Comparator.ID GetId()
    {
        return this.Id;
    }
}

