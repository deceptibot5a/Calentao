using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering.Universal;

public class BikeUpController : MonoBehaviour
{
    public string _animationBoolName; 
    public bool caninteract = false;
    public ExploradorAnimatorManager exploradorAnimatorManager;
    public ExploradorPlayerController exploradorPlayerController;
    public ExploradorCameraManager exploradorCameraManager;
    public CharacterController characterController; 
    public Animator PlayerAnimator;
    public GameObject PlayerGameObject;
    public GameObject bikeCamera; 
    public GameObject bike;
    public Transform referenceTransform;
    public GameObject camera; 
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (caninteract))
        {
            bikeCamera.SetActive(true);
            camera.SetActive(false);
            Debug.Log(_animationBoolName);
            characterController.enabled = false; 
            PlayerGameObject.transform.parent = bike.transform;
            PlayerGameObject.transform.position = referenceTransform.position; 
            PlayerGameObject.transform.rotation = referenceTransform.rotation;
            caninteract = false;
            exploradorAnimatorManager.enabled = false;
            exploradorCameraManager.enabled = false;
            exploradorPlayerController.enabled = false; 
            PlayerAnimator.SetBool(_animationBoolName, true);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Debug.Log("Can " + _animationBoolName);
            caninteract = true; 


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Debug.Log("Jugador sale");
  
        }
    }
}
