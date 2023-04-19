using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Calentao.PlayerContol;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class InteractionsPlayer2 : MonoBehaviour
{
    public CinemachineVirtualCamera activecamera;
    public GameObject interactUi;
    public GameObject crosshair;
    public PuzzleInteractions2 puzzle;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject buttonCamera;
    private PlayerController playerController;
    public bool correct;
    private bool assigned;
    [SerializeField] private float AudioFadeSpeed = 1f;
    public AudioSource[] audioSources;
    public Animator animator;
    public GameObject playerObject;
    private PhotonView photonView;
    public int puzzletype;

    public Puzzle2 puzzle2;

    private void Start()
    {
        StartCoroutine(AssignPlayerInteractions());
    }


    IEnumerator AssignPlayerInteractions()
    {
        yield return new WaitForSeconds(0.1f);
        inputManager = GameObject.FindWithTag("Player2").GetComponent<InputManager>();
        interactUi = GameObject.Find("InteractableP2");
        crosshair = GameObject.Find("CrosshairP2");
        playerController = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
        playerObject = GameObject.FindWithTag("Player2");
        audioSources = playerObject.GetComponents<AudioSource>();
        animator = playerObject.GetComponent<Animator>();
        photonView = GameObject.FindWithTag("Player2").GetComponent<PhotonView>();
    }

    public void uiInteraction()
    {
        interactUi.GetComponent<CanvasGroup>().alpha = 1;
        inputManager.caninteract = true;
    }

    public void uiInteractionOff()
    {
        interactUi.GetComponent<CanvasGroup>().alpha = 0;
        inputManager.caninteract = false;
    }



    public void interacted()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = 0;
        }

        activecamera.m_Priority = 10;

        animator.SetBool("Idle", true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        interactUi.GetComponent<CanvasGroup>().alpha = 0;



        inputManager.isinpuzzle = true;
        inputManager.caninteract = false;
        //inputManager.enabled = false;
        playerController.enabled = false;

        if (puzzletype == 2)
        {
            puzzle2.Puzzle2On();
        }

    }

    public void stopInteraction()
    {
        animator.SetBool("Idle", false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
        inputManager.isinpuzzle = false;
        //inputManager.enabled = true;
        playerController.enabled = true;

        activecamera.m_Priority = 0;

        if (correct)
        {
            buttonCamera.SetActive(false);
        }
        else
        {
            interactUi.SetActive(true);
            inputManager.caninteract = true;
            interactUi.GetComponent<CanvasGroup>().alpha = 1;
        }
        if (puzzletype == 2)
        {
            puzzle2.Puzzle2Off();
        }
    }
}
