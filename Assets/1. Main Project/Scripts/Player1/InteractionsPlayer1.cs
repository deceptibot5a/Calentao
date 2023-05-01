using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Calentao.PlayerContol;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Unity.VisualScripting;

public class InteractionsPlayer1 : MonoBehaviour
{
    public CinemachineVirtualCamera activecamera;
    public GameObject interactUi;
    public GameObject crosshair;
    public PuzzleInteractions puzzle;
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
    private ExploradorPlayerController playerControllerex;
    private ExploradorCameraManager playerCamera;
    private ExploradorAnimatorManager playerAnimator;



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
        interactUi = GameObject.Find("InteractableP1");
        crosshair = GameObject.Find("CrosshairP1");
        playerObject = GameObject.FindWithTag("Player1");
        audioSources = playerObject.GetComponents<AudioSource>();
        animator = GameObject.Find("ExploradorFINALv1").GetComponent<Animator>();
        photonView = GameObject.FindWithTag("Player1").GetComponent<PhotonView>();
        playerControllerex = GameObject.FindWithTag("Player1").GetComponent<ExploradorPlayerController>();
        playerCamera = GameObject.Find("CM vcam1 explorador").GetComponent<ExploradorCameraManager>();
        playerAnimator = GameObject.Find("ExploradorFINALv1").GetComponent<ExploradorAnimatorManager>();
        
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
        playerControllerex.enabled = false;
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
        playerControllerex.enabled = true;
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
