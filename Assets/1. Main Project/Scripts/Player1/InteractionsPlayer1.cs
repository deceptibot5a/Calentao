using System;
using System.Collections;
using System.Collections.Generic;
using Calentao.PlayerContol;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class InteractionsPlayer1 : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    public GameObject interactUi;
    public GameObject crosshair;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject buttonCamera;
    private PlayerController playerController;
    public bool correct;
    private bool assigned;
    [SerializeField] private float AudioFadeSpeed = 1f;
    public AudioSource[] audioSources;
    public Animator animator; 
    public GameObject playerObject;
   
    
    void Update()
    {
        if (assigned == false)
        {
            inputManager = GameObject.FindWithTag("Player1").GetComponent<InputManager>();
            interactUi = GameObject.Find("Interactable");
            crosshair = GameObject.Find("Crosshair");
            playerController = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
            playerObject = GameObject.FindWithTag("Player1");
            audioSources = playerObject.GetComponents<AudioSource>();
            animator = playerObject.GetComponent<Animator>();
        }
        if (inputManager && interactUi && crosshair && playerController && playerObject && animator  == null)
        {
            assigned = false;
        }
        else
        {
            assigned = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            interactUi.GetComponent<CanvasGroup>().alpha = 1;
            inputManager.caninteract = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            interactUi.GetComponent<CanvasGroup>().alpha = 0;
            inputManager.caninteract = false;
        }
    }

    public void interacted()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = 0; 
        }
        animator.SetBool("Idle", true);
        vcam.m_Priority = 50;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        interactUi.SetActive(false);
        inputManager.isinpuzzle = true;
        inputManager.caninteract = false;
        //inputManager.enabled = false;
        playerController.enabled = false;

    }
    
    public void stopInteraction()
    {
        animator.SetBool("Idle", false);
        vcam.m_Priority = 5;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
        inputManager.isinpuzzle = false;
        //inputManager.enabled = true;
        playerController.enabled = true;
        
        if (correct)
        { 
            buttonCamera.SetActive(false);
        }
        else
        {
            interactUi.SetActive(true);
            inputManager.caninteract = true;
        }
    }
    
}