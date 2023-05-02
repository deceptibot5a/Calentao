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
    
    private void Start()
    {
        StartCoroutine(AssignUi());
    }

    private void Update()
    {
        if (readyToInteract && Input.GetKeyDown(KeyCode.E))
        {
            puzzleCamera.Priority = 10;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            crosshair.SetActive(false);
            interactUI.GetComponent<CanvasGroup>().alpha = 0;
            playerControllergu.enabled = false;
            playerCamera.enabled = false;
            playerAnimator.enabled = false;
            StartCoroutine(ChangeBrainBlending());
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
            brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            ResetCamerasPriority();
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
                break;
            case 2:
                activeCamera1.Priority = 0;
                activeCamera2.Priority = 11;
                activeCamera3.Priority = 0;
                activeCamera4.Priority = 0;
                break;
            case 3:
                activeCamera1.Priority = 0;
                activeCamera2.Priority = 0;
                activeCamera3.Priority = 11;
                activeCamera4.Priority = 0;
                break;
            case 4:
                activeCamera1.Priority = 0;
                activeCamera2.Priority = 0;
                activeCamera3.Priority = 0;
                activeCamera4.Priority = 11;
                break;
        }
    }

    IEnumerator ChangeBrainBlending()
    {
        yield return new WaitForSeconds(2f);
        brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
    }

    public void ResetCamerasPriority()
    {
        activeCamera1.Priority = 0;
        activeCamera2.Priority = 0;
        activeCamera3.Priority = 0;
        activeCamera4.Priority = 0;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            readyToInteract = true;
            interactUI.GetComponent<CanvasGroup>().alpha = 1; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
     if (other.gameObject.CompareTag("Player2"))
     {
         readyToInteract = false;
         interactUI.GetComponent<CanvasGroup>().alpha = 0;
     }
    }
}

