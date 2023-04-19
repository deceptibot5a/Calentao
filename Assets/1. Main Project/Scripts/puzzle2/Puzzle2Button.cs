using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Puzzle2Button : MonoBehaviour
{
    
    
    [SerializeField] Material newMaterial; // The material you want to change to.
    [SerializeField] Material originalMaterial; // The original material of the object.
    [SerializeField] private PhotonView photonView;
    public static Puzzle2Button instance;
    

    private void Start()
    {
        instance = this;
        manager = GameObject.FindObjectOfType<Puzzle2>();
        photonView = GetComponent<PhotonView>();
    }

    [SerializeField] private float changeDuration = 2f; // The duration of the material change in seconds.

    private Renderer renderer;

    [SerializeField] private GameObject plataforma;
    
    private Puzzle2 manager;

    private bool shouldCheck;

    
    
    public void Buttonclicked()
    {
        photonView.RPC("Buttonclick", RpcTarget.All);
    }
    
    [PunRPC]
    public void Buttonclick()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = newMaterial;
        StartCoroutine(ChangeMaterialBack());
        plataforma.SetActive(true);
        manager.AddObject(plataforma);
        shouldCheck = true;
        StartCoroutine(checking());

    }
    
    
    IEnumerator checking() {
        while (shouldCheck) {
            
            if (!plataforma.activeSelf)
            {
                renderer.material = originalMaterial;
                shouldCheck = false;
            }

            yield return new WaitForSeconds(0.5f); // Wait for half a second
        }
    }

    private IEnumerator ChangeMaterialBack()
    {
        yield return new WaitForSeconds(changeDuration);
        plataforma.SetActive(false);

    }

public void DeactivatePlatform()
{
    renderer.material = originalMaterial;
    plataforma.SetActive(false);
}

}

