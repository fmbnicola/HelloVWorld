using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Material OpenLightMaterial;
    public Material ClosedLightMaterial;

    public Renderer FrameRenderer;
    public Renderer ShutterRenderer;

    private float animTime = 0.0f;
    private float target_animTime = 0.0f;
    public float animSpeed = 1.0f;

    private MaterialPropertyBlock propertyBlock;

    // Start is called before the first frame update
    void Start()
    {
        var frame = transform.Find("Frame");
        FrameRenderer = frame.GetComponent<Renderer>();

        var shutter = frame.transform.Find("Shutter");
        ShutterRenderer = shutter.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Animate animTime parameter
        animTime = Mathf.Lerp(animTime, target_animTime, animSpeed * Time.deltaTime);

        //set property block for door
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();

        propertyBlock.SetFloat("_AnimationTime", animTime);
        ShutterRenderer.SetPropertyBlock(propertyBlock);
    }

    public void Open()
    {
        Debug.Log("Open Door");
        target_animTime = 1.0f;

        //change light material
        var mats = FrameRenderer.materials;
        mats[1] = OpenLightMaterial;
        FrameRenderer.materials = mats;
    }

    public void Close()
    {
        Debug.Log("Close Door");
        target_animTime = 0.0f;

        //change light material
        var mats = FrameRenderer.materials;
        mats[1] = ClosedLightMaterial;
        FrameRenderer.materials = mats; ;
    }
}
