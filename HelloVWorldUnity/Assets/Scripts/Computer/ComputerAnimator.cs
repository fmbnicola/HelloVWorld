using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAnimator : MonoBehaviour
{

    public GameObject Dais;
    private Renderer DaisRenderer;
    private Material DaisMaterial;

    public GameObject HoloField;
    private Renderer HoloFieldRenderer;
    private Material HoloFieldMaterial;

    private float animTime = 0.0f;
    private float target_animTime = 0.0f;
    public float animSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        DaisRenderer = Dais.GetComponent<Renderer>();
        DaisMaterial = DaisRenderer.material;

        HoloFieldRenderer = HoloField.GetComponent<Renderer>();
        HoloFieldMaterial = HoloFieldRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {

        //Animate animTime parameter
        animTime = Mathf.Lerp(animTime, target_animTime, animSpeed * Time.deltaTime);
        DaisMaterial.SetFloat("_AnimationTime", animTime);
        HoloFieldMaterial.SetFloat("_AnimationTime", animTime);
    }

    public void TurnOn()
    {
        target_animTime = 1.0f;
    }

    public void TurnOff()
    {
        target_animTime = 0.0f;
    }
}
