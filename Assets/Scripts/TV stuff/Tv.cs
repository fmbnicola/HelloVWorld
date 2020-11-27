using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tv : MonoBehaviour
{
    public bool BlocksTab = true;
    public bool ConditionTab = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckTv();
    }

    private void CheckTv()
    {
        if (!this.BlocksTab) this.transform.Find("BlockPlane").gameObject.SetActive(false);
    }
}
