using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using SudoProgram;




public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonPrefab;

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

    
    public enum States
    {
        Categories,
        Programming,
        Condition
    }

    public States State = States.Categories;


    public List<ProgrammingBlock> AvailableProgrammingBlocks;
    public List<ConditionBlock> AvailableConditionBlocks;

    public Transform SpawnPoint;


    [SerializeField]
    public GameObject CategoriesParent;

    [SerializeField]
    public GameObject ProgrammingParent;

    [SerializeField]
    public GameObject ConditionParent;


    private Vector2 CanvasSize;
    private Vector2 ButtonSize;

    public Vector2Int GridDimensions = new Vector2Int(4, 3);

    private Vector2 Margins = new Vector2(0,0);

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


        this.InitGridDimensions();


        this.GenerateButtonsProgramming();

        this.GenerateButtonsCondition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Buttons
    private void InitGridDimensions()
    {
        var rect = this.GetComponent<RectTransform>();

        this.CanvasSize = rect.sizeDelta;

        var buttonRect = this.ButtonPrefab.GetComponent<RectTransform>();

        this.ButtonSize = buttonRect.sizeDelta;


        var maxButtonH = Mathf.FloorToInt(((this.CanvasSize.x / this.ButtonSize.x) - 0.25f) / 1.25f);
        var maxButtonV = Mathf.FloorToInt(((this.CanvasSize.y / this.ButtonSize.y) - 0.25f) / 1.25f);

        this.GridDimensions.x = (int) Mathf.Min(this.GridDimensions.x, maxButtonH);
        this.GridDimensions.y = (int) Mathf.Min(this.GridDimensions.y, maxButtonV);

        this.Margins.x = (this.CanvasSize.x - this.GridDimensions.x * this.ButtonSize.x) / (this.GridDimensions.x + 1);
        this.Margins.y = (this.CanvasSize.y - this.GridDimensions.y * this.ButtonSize.y) / (this.GridDimensions.y + 1);
    }


    private GameObject GenerateButton(Transform parent, float x, float y, int index, int quantity)
    {
        var button = Instantiate(this.ButtonPrefab);

        #region Transform SetUp
        button.transform.SetParent(parent, false);

        button.name = index.ToString();

        var rectTransform = button.GetComponent<RectTransform>();

        rectTransform.localPosition    = new Vector3( x, y, 0);
        rectTransform.localEulerAngles = Vector3.zero;
        #endregion

        #region Stock
        var stockObj = button.transform.Find("Stock");

        var str = quantity.ToString();

        stockObj.GetComponent<TextMeshProUGUI>().text = str + "/" + str;
        #endregion

        #region Call Back
        var uiButton = button.GetComponent<Button>();
        if (parent.name == "ProgrammingBlocks") uiButton.onClick.AddListener(() => this.SpawnProgrammingBlock(index));
        if (parent.name == "ConditionBlocks")   uiButton.onClick.AddListener(() => this.SpawnConditionBlock(index));
        #endregion

        return button.gameObject;
    }


    private void GenerateButtonsProgramming()
    {
        var x = -this.CanvasSize.x / 2;
        var y =  this.CanvasSize.y / 2;

        var rowI    = 0;
        var columnI = 0;

        for(var i = 0; i < this.AvailableProgrammingBlocks.Count; i++)
        {
            var block = this.AvailableProgrammingBlocks[i];

            float buttonX = x + (columnI + 1) * this.Margins.x + (columnI + 0.5f) * this.ButtonSize.x;
            float buttonY = y - (rowI    + 1) * this.Margins.y - (rowI    + 0.5f) * this.ButtonSize.y;

            this.GenerateButton(this.ProgrammingParent.transform, buttonX, buttonY, i, block.GetAvailable());

            columnI++;
            if(columnI == this.GridDimensions.x)
            {
                columnI = 0;
                rowI++;

                if (rowI == this.GridDimensions.y) break;
            }
        }
    }

    private void GenerateButtonsCondition()
    {
        var x = -this.CanvasSize.x / 2;
        var y = this.CanvasSize.y / 2;

        var rowI = 0;
        var columnI = 0;

        for (var i = 0; i < this.AvailableConditionBlocks.Count; i++)
        {
            var block = this.AvailableConditionBlocks[i];

            float buttonX = x + (columnI + 1) * this.Margins.x + (columnI + 0.5f) * this.ButtonSize.x;
            float buttonY = y - (rowI    + 1) * this.Margins.y - (rowI    + 0.5f) * this.ButtonSize.y;

            this.GenerateButton(this.ConditionParent.transform, buttonX, buttonY, i, block.GetAvailable());

            columnI++;
            if (columnI == this.GridDimensions.x)
            {
                columnI = 0;
                rowI++;

                if (rowI == this.GridDimensions.y) break;
            }
        }
    }


    private void UpdateStockProgramming(int index)
    {
        var block = this.AvailableProgrammingBlocks[index];

        var stored    = block.Stored;
        var available = block.GetAvailable();

        var buttonObj = this.ProgrammingParent.transform.Find(index.ToString());

        var stockObj = buttonObj.transform.Find("Stock");

        stockObj.GetComponent<TextMeshProUGUI>().text = stored + "/" + available;

        var uiButton = buttonObj.GetComponent<Button>();

        if (uiButton.interactable == false && stored > 0) uiButton.interactable = true;
        if (stored == 0) uiButton.interactable = false;
    }

    private void UpdateStockCondition(int index)
    {
        var block = this.AvailableConditionBlocks[index];

        var stored = block.Stored;
        var available = block.GetAvailable();

        var buttonObj = this.ConditionParent.transform.Find(index.ToString());

        var stockObj = buttonObj.transform.Find("Stock");

        stockObj.GetComponent<TextMeshProUGUI>().text = stored + "/" + available;

        var uiButton = buttonObj.GetComponent<Button>();

        if (uiButton.interactable == false && stored > 0) uiButton.interactable = true;
        if (stored == 0) uiButton.interactable = false;
    }
    #endregion


    #region State Transitions
    public void Return()
    {
        this.State = States.Categories;

        this.CategoriesParent.SetActive(true);

        this.ProgrammingParent.SetActive(false);
        this.ConditionParent.SetActive(false);
    }

    public void GoToProgramming()
    {
        this.State = States.Programming;

        this.CategoriesParent.SetActive(false);

        this.ProgrammingParent.SetActive(true);
    }

    public void GoToCondition()
    {
        this.State = States.Condition;

        this.CategoriesParent.SetActive(false);

        this.ConditionParent.SetActive(true);

    }
    #endregion


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

            this.UpdateStockProgramming(index);

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

            this.UpdateStockCondition(index);

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

        this.AvailableProgrammingBlocks[index] = block;

        this.UpdateStockProgramming(index);
    }

    void DepositConditionBlock(int index)
    {
        var block = this.AvailableConditionBlocks[index];

        block.Stored += 1;

        var available = block.GetAvailable();
        if (block.Stored > available) block.Stored = available;

        this.AvailableConditionBlocks[index] = block;

        this.UpdateStockCondition(index);
    }
    #endregion


    public void TestSpawn()
    {
        this.SpawnProgrammingBlock(0);
    }
}
