using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour
{
    public GameObject threads = null;
    public float speed = 0.3f;

    private Renderer ThreadRenderer;
    private Material ThreadMaterial;

    enum FaceExpression
    {
        Normal,
        Excited,
        Mad,
        Sad
    }

    public GameObject face = null;
    public int expression = 0;

    private Renderer FaceRenderer;
    private Material FaceMaterial;

    // Start is called before the first frame update
    void Start()
    {
        ThreadRenderer = threads.GetComponent<Renderer>();
        List<Material> mats = new List<Material>();
        ThreadRenderer.GetMaterials(mats);

        ThreadMaterial = mats[1];

        FaceRenderer = face.GetComponent<Renderer>();
        FaceMaterial = FaceRenderer.material;
    }

    // Set threads to moving foward
    public void ThreadsFoward()
    {
        ThreadMaterial.SetFloat("_Speed", speed);
    }

    // Set threads to moving in reverse
    public void ThreadsReverse()
    {
        ThreadMaterial.SetFloat("_Speed", -speed);
    }

    // Set threads to stop moving
    public void ThreadsStop()
    {
        ThreadMaterial.SetFloat("_Speed", 0);
    }

    // Make sad face
    public void FaceSad(float expressionTime = 1.0f)
    {
        FaceMaterial.SetInt("_Face", (int) FaceExpression.Sad);
        StartCoroutine(ResetFace(expressionTime));
    }

    // Make excited face
    public void FaceExcited(float expressionTime = 1.0f)
    {
        FaceMaterial.SetInt("_Face", (int)FaceExpression.Excited);
        StartCoroutine(ResetFace(expressionTime));
    }

    // Make mad face
    public void FaceMad(float expressionTime = 1.0f)
    {
        FaceMaterial.SetInt("_Face", (int)FaceExpression.Mad);
        StartCoroutine(ResetFace(expressionTime));
    }

    private IEnumerator ResetFace(float time)
    {
        yield return new WaitForSeconds(time);
        FaceMaterial.SetInt("_Face", (int) FaceExpression.Normal);
        yield break;
    }
}
