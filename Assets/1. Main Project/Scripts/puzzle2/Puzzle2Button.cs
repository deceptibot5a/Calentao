using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Puzzle2Button : MonoBehaviour
{
    [SerializeField] Material newMaterial;
    [SerializeField] Material originalMaterial;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private float changeDuration = 2f;
    [SerializeField] private GameObject plataforma;
    
    public static Puzzle2Button instance;
    
    private Renderer renderer;
    private Puzzle2 manager;
    private bool shouldCheck;
    public bool Highlighted = false;
    private void Start()
    {
        instance = this;
        manager = GameObject.FindObjectOfType<Puzzle2>();
        photonView = GetComponent<PhotonView>();
    }
    
    private void OnMouseDown()
    {
        if (Highlighted)
        {
            Buttonclicked();
            Debug.Log("clickeado");
        }
    }
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
