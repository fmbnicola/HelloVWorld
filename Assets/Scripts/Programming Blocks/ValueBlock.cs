using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueBlock : ProgrammingBlock
{
    [SerializeField]
    private Value.ID Id = Value.ID.Wall;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
