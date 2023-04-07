using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Button : MonoBehaviour
{
    [SerializeField] Material newMaterial; // The material you want to change to.
    [SerializeField] Material originalMaterial; // The original material of the object.

    public float changeDuration = 2f; // The duration of the material change in seconds.

    private Renderer renderer;

    [SerializeField] private GameObject plataforma;

    // This function changes the material of the object.
    public void buttonclick()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = newMaterial;
        StartCoroutine(ChangeMaterialBack());
        plataforma.SetActive(true);
    }

    private IEnumerator ChangeMaterialBack()
    {
        yield return new WaitForSeconds(changeDuration);
        renderer.material = originalMaterial;
        plataforma.SetActive(false);
    }
}

