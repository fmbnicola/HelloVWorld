using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SudoProgram;




public class BlockManager : MonoBehaviour
{ 

    [System.Serializable]
    public struct ProgrammingBlock
    {
        [SerializeField]
        GameObject Prefab;

        [SerializeField]
        int Available;

        [Header("Only used if Preafab is 'InstructionBlock'."), SerializeField]
        public Instruction.ID InstructionId;


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



    [System.Serializable]
    public struct ConditionBlock
    {
        [SerializeField]
        GameObject Prefab;

        [SerializeField]
        int Available;

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



    public List<ProgrammingBlock> AvailableProgrammingBlocks;
    public List<ConditionBlock> AvailableConditionBlocks;


    public Transform SpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i < this.AvailableProgrammingBlocks.Count; i++)
        {
            var block = this.AvailableProgrammingBlocks[i];

            block.Stored = block.GetAvailable();

            this.AvailableProgrammingBlocks[i] = block;
        }

        for (var i = 0; i < this.AvailableConditionBlocks.Count; i++)
        {
            var block = this.AvailableConditionBlocks[i];

            block.Stored = block.GetAvailable();

            this.AvailableConditionBlocks[i] = block;
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
    GameObject SpawnProgrammingBlock(int index)
    {
        if (!this.WithdrawProgrammingBlock(index)) return null;

        var block = this.AvailableProgrammingBlocks[index];

        var prefab = block.GetPrefab();

        var inst = Instantiate(prefab) as GameObject;

        inst.transform.position = this.SpawnPoint.position;
        inst.transform.rotation = this.SpawnPoint.rotation;

        switch(prefab.name)
        {
            default:
                break;

            case "InstructionBlock":
                var instruction = inst.GetComponent<Block.InstructionBlock>();
                instruction.Convert(block.InstructionId);
                break;

        }

        return inst;
    }



    GameObject SpawnConditionBlock(int index)
    {
        if (!this.WithdrawConditionBlock(index)) return null;

        var block = this.AvailableConditionBlocks[index];

        var prefab = block.GetPrefab();

        var inst = Instantiate(prefab) as GameObject;

        inst.transform.position = this.SpawnPoint.position;
        inst.transform.rotation = this.SpawnPoint.rotation;

        switch (prefab.name)
        {
            default:
                break;

            case "Comparator":
                var comparator = inst.GetComponent<Block.ComparatorBlock>();
                comparator.Convert(block.ComparatorId);
                break;

            case "SensorBlock":
                var sensor = inst.GetComponent<Block.SensorBlock>();
                sensor.Convert(block.SensorId);
                break;

            case "ValueBlock":
                var value = inst.GetComponent<Block.ValueBlock>();
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

        for(var i = 0; i < this.AvailableProgrammingBlocks.Count; i++)
        {
            if(this.AvailableProgrammingBlocks[i].GetPrefab().name == name)
            {
                this.DepositProgrammingBlock(i);
                return;
            }
        }

        for (var i = 0; i < this.AvailableConditionBlocks.Count; i++)
        {
            if (this.AvailableConditionBlocks[i].GetPrefab().name == name)
            {
                this.DepositConditionBlock(i);
                return;
            }
        }
    }
    #endregion


    #region Block Methods

    bool WithdrawProgrammingBlock(int index)
    {
        var block = this.AvailableProgrammingBlocks[index];

        if (block.Stored > 0)
        {
            block.Stored -= 1;

            this.AvailableProgrammingBlocks[index] = block;

            return true;
        }

        return false;
    }

    bool WithdrawConditionBlock(int index)
    {
        var block = this.AvailableConditionBlocks[index];

        if (block.Stored > 0)
        {
            block.Stored -= 1;

            this.AvailableConditionBlocks[index] = block;

            return true;
        }

        return false;
    }

    void DepositProgrammingBlock(int index)
    {
        var block = this.AvailableProgrammingBlocks[index];

        block.Stored += 1;

        var available = block.GetAvailable();
        if (block.Stored > available) block.Stored = available;
    }

    void DepositConditionBlock(int index)
    {
        var block = this.AvailableConditionBlocks[index];

        block.Stored += 1;

        var available = block.GetAvailable();
        if (block.Stored > available) block.Stored = available;
    }
    #endregion


    public void TestSpawn()
    {
        this.SpawnProgrammingBlock(0);
    }
}
