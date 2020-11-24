using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{

    public Material spawnMaterial;
    public float delayTime = 0.0f; 
    public float durationTime = 2.0f;

    public bool executing = false;

    private float elapsedTime = 0.0f;

    private GameObject realObject;
    private GameObject fakeObject;

    private MaterialPropertyBlock propertyBlock;
    private List<Renderer> renderers = new List<Renderer>();

    public void Initialize(GameObject obj, Material mat)
    {   
        realObject = obj;
        spawnMaterial = mat;

        //make fake copy of gameobject
        SaveFakeObject();
    }

    private void SaveFakeObjectAux(Transform parent_transform, Transform fake_transform, Transform real_transform)
    {
        // Copy Transform
        fake_transform.name = real_transform.name;

        fake_transform.position     = real_transform.position;
        fake_transform.rotation     = real_transform.rotation;
        fake_transform.localScale   = real_transform.lossyScale;

        if(parent_transform != null) fake_transform.parent = parent_transform;

        // Copy Filter
        var real_filter = real_transform.GetComponent<MeshFilter>();
        if(real_filter != null)
        {
            var fake_filter = fake_transform.gameObject.AddComponent<MeshFilter>();
            fake_filter.mesh = real_filter.mesh;
        }

        // Copy Mesh Renderer
        var real_renderer = real_transform.GetComponent<MeshRenderer>();
        if (real_renderer != null)
        {
            var fake_renderer = fake_transform.gameObject.AddComponent<MeshRenderer>();

            var mats = real_renderer.sharedMaterials;
            for(var i = 0; i < mats.Length; i++)
            {
                mats[i] = spawnMaterial;
            }
            fake_renderer.materials = mats;

            renderers.Add(fake_renderer);
        }
        
        // Explore Children
        foreach (Transform child_transform in real_transform)
        {
            var fake_child = new GameObject();
            SaveFakeObjectAux(fake_transform, fake_child.transform, child_transform);
        }
    }

    private void SaveFakeObject()
    {
        fakeObject = new GameObject();
        SaveFakeObjectAux(null, fakeObject.transform, realObject.transform);
        fakeObject.SetActive(false);
    }

    IEnumerator DelayRoutine()
    {
        //wait ammount of time specified.
        yield return new WaitForSeconds(delayTime);
        StartEffect();
    }

    public void Execute()
    {
        StartEffect();
    }

    private void CopyTransform()
    {
        fakeObject.transform.position = realObject.transform.position;
        fakeObject.transform.rotation = realObject.transform.rotation;
        fakeObject.transform.localScale = realObject.transform.lossyScale;
    }

    private void StartEffect()
    {
        Debug.Log("Start Effect");

        CopyTransform();

        realObject.SetActive(false);
        fakeObject.SetActive(true);

        elapsedTime = 0.0f;
        executing = true;
    }

    private void SpawnRealObject()
    {
        realObject.SetActive(true);
    }

    private void EndEffect()
    {
        Debug.Log("End Effect");
        fakeObject.SetActive(false);

        executing = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (executing)
        {
            CopyTransform();

            elapsedTime += Time.deltaTime;
            if(elapsedTime >= durationTime * 0.5)
            {
                SpawnRealObject();
            }
            if(elapsedTime > durationTime)
            {
                EndEffect();
            }

            var animTime = elapsedTime / durationTime;

            //set property
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();

            propertyBlock.SetFloat("_AnimationTime", animTime);
            foreach (var renderer in this.renderers)
            {
                renderer.SetPropertyBlock(propertyBlock);
            }

            Debug.Log(animTime);
        }
    }
}
