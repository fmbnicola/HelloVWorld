using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparatorBlock : ProgrammingBlock
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

    }


    public Comparator Parse()
    {
        return new Comparator(this.Id);
    }
}

