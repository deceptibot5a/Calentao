using System;
using System.Collections;
using System.Collections.Generic;
using Calentao.PlayerContol;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class A : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    public GameObject interactUi;
    public GameObject Ui;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private GameObject buttonCamera;
    public bool correct;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUi.SetActive(true);
            _controller.caninteract = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUi.SetActive(false);
            _controller.caninteract = false;
        }
    }

    public void interacted()
    {
        vcam.m_Priority = 50;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Ui.SetActive(false);
        interactUi.SetActive(false);
        _controller.isinpuzzle = true;
        _controller.caninteract = false;

    }
    
    public void stopInteraction()
    {
        vcam.m_Priority = 5;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Ui.SetActive(true);
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