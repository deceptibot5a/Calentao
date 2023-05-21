using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    
    public CinemachineVirtualCamera activeCamera1, activeCamera2, activeCamera3, activeCamera4;
    public GameObject uiCamera1, uiCamera2, uiCamera3, uiCamera4;
    public CinemachineVirtualCamera puzzleCamera;
    public bool readyToInteract = false;
    
    public GameObject interactUI;
    private GuiaPlayerController playerControllergu;
    private GuiaCameraManager playerCamera;
    private GuiaAnimationManager playerAnimator;
    private PhotonView photonView;
    private GameObject playerObject;
    private GameObject crosshair;
    private AudioSource[] audioSources;
    private Animator animator;
    public CinemachineBrain brain;
    public Button activeCamerasButton;
    
    private void Start()
    {
        StartCoroutine(AssignUi());
    }

    private void Update()
    {
        if (readyToInteract && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ChangeBrainBlending(CinemachineBlendDefinition.Style.Cut));
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            crosshair.SetActive(false);
            interactUI.GetComponent<CanvasGroup>().alpha = 0;
            playerControllergu.enabled = false;
            playerCamera.enabled = false;
            playerAnimator.enabled = false;
            
        }
        if(readyToInteract && Input.GetKeyDown(KeyCode.F))
        {
            puzzleCamera.Priority = 0;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            crosshair.SetActive(true);
            interactUI.GetComponent<CanvasGroup>().alpha = 1;
            playerControllergu.enabled = true;
            playerCamera.enabled = true;
            playerAnimator.enabled = true;
            ResetCamerasPriorityAndUI();
        }
    }
    
    IEnumerator AssignUi()
    {
        yield return new WaitForSeconds(0.1f);
        interactUI = GameObject.Find("InteractableP2");
        crosshair = GameObject.Find("CrosshairP2");
        playerControllergu = GameObject.FindWithTag("Player2").GetComponent<GuiaPlayerController>();
        playerObject = GameObject.FindWithTag("Player2");
        audioSources = playerObject.GetComponents<AudioSource>();
        animator = GameObject.Find("GuiaMesh").GetComponent<Animator>();
        photonView = GameObject.FindWithTag("Player2").GetComponent<PhotonView>();
        playerCamera = GameObject.Find("CM vcam1 Guia").GetComponent<GuiaCameraManager>();
        playerAnimator = GameObject.Find("GuiaMesh").GetComponent<GuiaAnimationManager>();
        brain = GameObject.Find("GuiaCinemachineBrain").GetComponent<CinemachineBrain>();
    }
    
public void ChangeCamera(int camera)
    {
        switch (camera)
        {
            case 1:
                activeCamera1.Priority = 11;
                activeCamera2.Priority = 0;
                activeCamera3.Priority = 0;
                activeCamera4.Priority = 0;
                uiCamera1.SetActive(true);
                uiCamera2.SetActive(false);
                uiCamera3.SetActive(false);
                uiCamera4.SetActive(false);
                break;
            
            case 2:
                activeCamera1.Priority = 0;
                activeCamera2.Priority = 11;
                activeCamera3.Priority = 0;
                activeCamera4.Priority = 0;
                uiCamera1.SetActive(false);
                uiCamera2.SetActive(true);
                uiCamera3.SetActive(false);
                uiCamera4.SetActive(false);
                break;
            
            case 3:
                activeCamera1.Priority = 0;
                activeCamera2.Priority = 0;
                activeCamera3.Priority = 11;
                activeCamera4.Priority = 0;
                uiCamera1.SetActive(false);
                uiCamera2.SetActive(false);
                uiCamera3.SetActive(true);
                uiCamera4.SetActive(false);
                break;
           
            case 4:
                activeCamera1.Priority = 0;
                activeCamera2.Priority = 0;
                activeCamera3.Priority = 0;
                activeCamera4.Priority = 11;
                uiCamera1.SetActive(false);
                uiCamera2.SetActive(false);
                uiCamera3.SetActive(false);
                uiCamera4.SetActive(true);
                break;
        }
    }

    IEnumerator ChangeBrainBlending(CinemachineBlendDefinition.Style style)
    {
        yield return new WaitForSeconds(0.1f);
        brain.m_DefaultBlend.m_Style = style;
        yield return new WaitForSeconds(0.1f);
        ChangeCamera(1);
    }
    

    public void ResetCamerasPriorityAndUI()
    {
        activeCamera1.Priority = 0;
        activeCamera2.Priority = 0;
        activeCamera3.Priority = 0;
        activeCamera4.Priority = 0;
        uiCamera1.SetActive(false);
        uiCamera2.SetActive(false);
        uiCamera3.SetActive(false);
        uiCamera4.SetActive(false);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2") && photonView.IsMine)
        {
            readyToInteract = true;
            interactUI.GetComponent<CanvasGroup>().alpha = 1; 
            activeCamerasButton.interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
     if (other.gameObject.CompareTag("Player2"))
     {
         readyToInteract = false;
         interactUI.GetComponent<CanvasGroup>().alpha = 0;
            activeCamerasButton.interactable = false;
     }
    }
}

