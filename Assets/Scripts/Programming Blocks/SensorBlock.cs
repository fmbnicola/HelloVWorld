using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBlock : ConditionBlock
{
    [SerializeField]
    private Sensor.ID Id = Sensor.ID.Camera;


    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<Rigidbody>().freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.FixRotation();
    }



    public Sensor Parse()
    {
        return new Sensor(this.Id);
    }

    public Sensor.ID GetId()
    {
        return this.Id;
    }
}

