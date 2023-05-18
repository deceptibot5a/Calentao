using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle2Button : MonoBehaviour
{
    [SerializeField] Material pressedButtonMaterial;
    [SerializeField] Material unpressedButtonMaterial;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private float changeDuration;
    [SerializeField] private GameObject plataforma;
    [SerializeField] public bool platformOn = false;
    public static Puzzle2Button _instance;
    
    public Renderer buttonRenderer;
    private Puzzle2 manager;
    private bool shouldCheck;
    public bool Highlighted = false;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        
        manager = GameObject.FindObjectOfType<Puzzle2>();
        photonView = GetComponent<PhotonView>();
        plataforma.transform.localScale = new Vector3(0, 0f, 0f);
        //plataforma.GetComponent<BoxCollider>().enabled = false;
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
        buttonRenderer = GetComponent<Renderer>();
        buttonRenderer.material = pressedButtonMaterial;
        StartCoroutine(ChangeMaterialBack());
        LeanTween.scale(plataforma, new Vector3(2.8f, 0.3f, 2.8f), 0.5f).setEaseInSine();
        platformOn = true;
        manager.AddObject(plataforma);
        shouldCheck = true;
        plataforma.GetComponent<BoxCollider>().enabled = true;
        //StartCoroutine(checking());

    }
    
    
    /*IEnumerator checking() {
        while (shouldCheck) 
        {
            
            if (platformOn == false)
            {
                buttonRenderer.material = unpressedButtonMaterial;
                shouldCheck = false;
            }

            yield return new WaitForSeconds(0.5f); // Wait for half a second
        }
    }*/

    private IEnumerator ChangeMaterialBack()
    {
        yield return new WaitForSeconds(changeDuration);
        DeactivatePlatform();
    }

    public void DeactivatePlatform()
    {
        buttonRenderer.material = unpressedButtonMaterial;
        LeanTween.scale(plataforma, new Vector3(0f, 0f, 0f), 0.5f).setEaseOutSine();
        platformOn = false;
        plataforma.GetComponent<BoxCollider>().enabled = false;
    }

}