using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Block
{
    [SerializeField]
    GameObject Prefab;

    [SerializeField]
    int Available;

    [Header("Only used if Preafab is 'InstructionBlock'."), SerializeField]
    public Instruction.ID InstructionId;

    [Header("Only used if Preafab is 'ComparatorBlock'."), SerializeField]
    public Comparator.ID ComparatorId;

    [Header("Only used if Preafab is 'SensorBlock'."), SerializeField]
    public Sensor.ID SensorId;

    [Header("Only used if Preafab is 'ValueBlock'."), SerializeField]
    public Value.ID ValueId;

    public int Stored;


    public GameObject GetPrefab()
    {
        return this.Prefab;
    }

    public int GetAvailable()
    {
        return this.Available;
    }
}


public class BlockManager : MonoBehaviour
{
    public List<Block> AvailableBlocks;

    public Transform SpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i < this.AvailableBlocks.Count; i++)
        {
            this.Init(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.Display();
    }


    // Renders the Block Grid
    void Display()
    {

    }


    #region Spawning
    GameObject Spawn(int index)
    {
        if (!this.Withdraw(index)) return null;

        var block = this.AvailableBlocks[index];

        var prefab = block.GetPrefab();

        var inst = Instantiate(prefab) as GameObject;

        inst.transform.position = this.SpawnPoint.position;
        inst.transform.rotation = this.SpawnPoint.rotation;

        switch(prefab.name)
        {
            default:
                break;

            case "InstructionBlock":
                var instruction = inst.GetComponent<InstructionBlock>();
                instruction.Convert(block.InstructionId);
                break;

            case "Comparator":
                var comparator = inst.GetComponent<ComparatorBlock>();
                comparator.Convert(block.ComparatorId);
                break;

            case "SensorBlock":
                var sensor = inst.GetComponent<SensorBlock>();
                sensor.Convert(block.SensorId);
                break;

            case "ValueBlock":
                var value = inst.GetComponent<ValueBlock>();
                value.Convert(block.ValueId);
                break;
        }

        return inst;
    }

    void DeSpawn(GameObject obj)
    {
        var name = obj.name;
        name = name.Split('(')[0];

        Destroy(obj);

        for(var i = 0; i < this.AvailableBlocks.Count; i++)
        {
            if(this.AvailableBlocks[i].GetPrefab().name == name)
            {
                this.Deposit(i);
            }
        }
    }
    #endregion


    #region Block Methods
    void Init(int index)
    {
        var block = this.AvailableBlocks[index];

        block.Stored = block.GetAvailable();

        this.AvailableBlocks[index] = block;
    }

    bool Withdraw(int index)
    {
        var block = this.AvailableBlocks[index];

        if (block.Stored > 0)
        {
            block.Stored -= 1;

            this.AvailableBlocks[index] = block;

            return true;
        }

        return false;
    }

    void Deposit(int index)
    {
        var block = this.AvailableBlocks[index];

        block.Stored += 1;

        var available = block.GetAvailable();
        if (block.Stored > available) block.Stored = available;
    }
    #endregion


    public void TestSpawn()
    {
        this.Spawn(0);
    }
}
