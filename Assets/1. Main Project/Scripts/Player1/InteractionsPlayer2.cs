using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Calentao.PlayerContol;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Unity.VisualScripting;

public class InteractionsPlayer2 : MonoBehaviour
{
    public CinemachineVirtualCamera activecamera;
    public GameObject interactUi;
    public GameObject crosshair;
    public PuzzleInteractions2 puzzle;
    [SerializeField] private GameObject buttonCamera;
    public bool correct;
    private bool assigned;
    [SerializeField] private float AudioFadeSpeed = 1f;
    public AudioSource[] audioSources;
    public Animator animator;
    public GameObject playerObject;
    private PhotonView photonView;
    public int puzzletype;
    public bool caninteract = false;
    public bool isinpuzzle = false;
    public Puzzle2 puzzle2;
    private GuiaPlayerController playerControllergu;
    private GuiaCameraManager playerCamera;
    private GuiaAnimationManager playerAnimator;

    private void Start()
    {
        StartCoroutine(AssignPlayerInteractions());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (caninteract))
        {
            interacted();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && (isinpuzzle))
        {
            stopInteraction();
        }
    }

    IEnumerator AssignPlayerInteractions()
    {
        yield return new WaitForSeconds(0.1f);
        interactUi = GameObject.Find("InteractableP2");
        crosshair = GameObject.Find("CrosshairP2");
        playerControllergu = GameObject.FindWithTag("Player2").GetComponent<GuiaPlayerController>();
        playerObject = GameObject.FindWithTag("Player2");
        audioSources = playerObject.GetComponents<AudioSource>();
        animator = GameObject.Find("GuiaMesh").GetComponent<Animator>();
        photonView = GameObject.FindWithTag("Player2").GetComponent<PhotonView>();
        playerCamera = GameObject.Find("CM vcam1 Guia").GetComponent<GuiaCameraManager>();
        playerAnimator = GameObject.Find("GuiaMesh").GetComponent<GuiaAnimationManager>();
    }

    public void uiInteraction()
    {
        interactUi.GetComponent<CanvasGroup>().alpha = 1;
        caninteract = true;
    }

    public void uiInteractionOff()
    {
        interactUi.GetComponent<CanvasGroup>().alpha = 0;
    }



    public void interacted()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = 0;
        }

        activecamera.m_Priority = 10;
        
        animator.SetBool("IsErect", true);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        interactUi.GetComponent<CanvasGroup>().alpha = 0;
        isinpuzzle = true;
        caninteract = false;
        playerControllergu.enabled = false;
        playerCamera.enabled = false;
        playerAnimator.enabled = false;

        Debug.Log("se desactivo");
        

        if (puzzletype == 2)
        {
            puzzle2.Puzzle2On();
        }
        
        if (puzzletype == 1)
        {
            puzzle2.Puzzle2On();
        }

    }

    public void stopInteraction()
    {
        activecamera.m_Priority = 0;


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
        isinpuzzle = false;
        playerControllergu.enabled = true;
        playerCamera.enabled = true;
        playerAnimator.enabled = true;

        animator.SetBool("IsErect", false);
        
        

        if (correct)
        {
            buttonCamera.SetActive(false);
            
        }
        else
        {
            interactUi.SetActive(true);
            caninteract = true;
            interactUi.GetComponent<CanvasGroup>().alpha = 1;
        }
        if (puzzletype == 2)
        {
            puzzle2.Puzzle2Off();
        }
    }
}