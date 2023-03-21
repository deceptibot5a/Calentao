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
    [SerializeField] private PlayerController _controller;
    [SerializeField] private GameObject buttonCamera;
    public bool correct;
    private bool assigned;
    
    void Update()
    {
        if (assigned == false)
        {
            _controller = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
            interactUi = GameObject.Find("Interactable");
            crosshair = GameObject.Find("Crosshair");
            vcam = GameObject.FindWithTag("Player1").GetComponentInChildren<CinemachineVirtualCamera>();
            
        }
        if (_controller && interactUi && crosshair && vcam == null)
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
            _controller.caninteract = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            interactUi.GetComponent<CanvasGroup>().alpha = 0;
            _controller.caninteract = false;
        }
    }

    public void interacted()
    {
        vcam.m_Priority = 50;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        interactUi.SetActive(false);
        _controller.isinpuzzle = true;
        _controller.caninteract = false;

    }
    
    public void stopInteraction()
    {
        vcam.m_Priority = 5;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
        _controller.isinpuzzle = false;
        if (correct)
        { 
            buttonCamera.SetActive(false);
        }
        else
        {
            interactUi.SetActive(true);
            _controller.caninteract = true;
        }
        

    }
}