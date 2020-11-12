using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBlock : ProgrammingBlock
{
    [SerializeField]
    private Sensor.ID Id = Sensor.ID.Camera;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public Sensor Parse()
    {
        return new Sensor(this.Id);
    }
}

