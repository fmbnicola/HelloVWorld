using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparatorBlock : ProgrammingBlock
{
    [SerializeField]
    private Comparator.ID Id = Comparator.ID.Equals;

    private MaterialPropertyBlock propertyBlock;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetInt("_Comparator", (int)Id);

        var symbol = transform.Find("Symbol");
        var renderer = symbol.GetComponent<Renderer>();
        renderer.SetPropertyBlock(propertyBlock);
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

