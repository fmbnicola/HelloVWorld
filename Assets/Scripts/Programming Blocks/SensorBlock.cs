﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBlock : ConditionBlock
{
    [SerializeField]
    private Sensor.ID Id = Sensor.ID.Camera;

    private MaterialPropertyBlock propertyBlock;

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

    private void OnValidate()
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetInt("_Sensor", (int)Id);

        var symbol = transform.Find("Symbol");
        var renderer = symbol.GetComponent<Renderer>();
        renderer.SetPropertyBlock(propertyBlock);
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

