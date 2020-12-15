using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    [SerializeField]
    private Door Door;

    [SerializeField]
    private string NextScene;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!this.Door.IsOpen()) return;

        var obj = other.gameObject;

        if(obj.name == "XR Rig" && this.NextScene != null && this.NextScene != "")
        {
            SceneManager.LoadScene(this.NextScene, LoadSceneMode.Single);
        }
    }
}
