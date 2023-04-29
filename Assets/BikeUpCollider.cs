using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BikeUpCollider : MonoBehaviour
{
    public Animator Playeranimator;
    public Transform transformLeft;
    public Transform transformRight;
    public GameObject bike; 
    public GameObject player;
    public GameObject AnimationManager;
    public GameObject CameraController; 
    public GameObject walkCamera;
    public GameObject upCamera;
    [SerializeField] private AudioSource _walkAudioSource;
    [SerializeField] private AudioSource _runAudioSource;
    [SerializeField] private AudioSource _breathingNormalAudioSource;
    [SerializeField] private AudioSource _breathingRunAudioSource;
    [SerializeField] private float AudioFadeSpeed = 1f;
    [SerializeField] private float BreathFadeSpeed = 0.2f;

    public void Start()
    {
        Playeranimator.GetComponent<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BikeLeft"))
        {
            _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 0.2f, BreathFadeSpeed * Time.deltaTime);
            _breathingRunAudioSource.volume = 0f; 
            _walkAudioSource.volume = 0f;
            _runAudioSource.volume = 0f;
            
            player.transform.parent = bike.transform; 
                walkCamera.SetActive(false);
                upCamera.SetActive(true);
                //player.transform.position = transformLeft.position; 
                Playeranimator.SetBool("EnterBikeLeft", true);
                ExploradorPlayerController exploradorController = player.GetComponent<ExploradorPlayerController>();
                exploradorController.enabled = false;
                ExploradorAnimatorManager exploradorAnimatorManager = AnimationManager.GetComponent<ExploradorAnimatorManager>();
                exploradorController.enabled = false;
                ExploradorCameraManager exploradorCameraManager = AnimationManager.GetComponent<ExploradorCameraManager>();
                exploradorController.enabled = false;
        }
        
        if (other.gameObject.CompareTag("BikeRight"))
                {
                    _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 0.2f, BreathFadeSpeed * Time.deltaTime);
                    _breathingRunAudioSource.volume = 0f; 
                    _walkAudioSource.volume = 0f;
                    _runAudioSource.volume = 0f;

                    player.transform.parent = bike.transform; 
                        walkCamera.SetActive(false);
                        upCamera.SetActive(true);
                        player.transform.position = transformRight.position;
                        player.transform.rotation = transformRight.rotation; 
                        Playeranimator.SetBool("EnterBikeRight", true);
                        ExploradorPlayerController exploradorController = player.GetComponent<ExploradorPlayerController>();
                        exploradorController.enabled = false;
                        ExploradorAnimatorManager exploradorAnimatorManager = AnimationManager.GetComponent<ExploradorAnimatorManager>();
                        exploradorController.enabled = false;
                        ExploradorCameraManager exploradorCameraManager = AnimationManager.GetComponent<ExploradorCameraManager>();
                        exploradorController.enabled = false;
                }
    }
}
