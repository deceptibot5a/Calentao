using System;
using System.Collections;
using System.Collections.Generic;
using AlekGames.HoverCraftSystem.Systems.Addons;
using AlekGames.HoverCraftSystem.Systems.Main;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Photon.Pun;

public class BikeUpManager : MonoBehaviour
{
    [Header("Player Scripts & References")]
    public bool caninteract = false;
    public bool canDown = false;
    public CanvasGroup fadeManager;
    public ExploradorPlayerController exploradorPlayerController;
    public ExploradorAnimatorManager exploradorAnimatorManager;
    public ExploradorCameraManager ExploradorCameraManager;
    public CharacterController characterController;
    public SkinnedMeshRenderer meshRenderer;
    public GameObject PlayerController; 
    public GameObject PlayerCamera;
    public Transform CameraTransform; 
    public CinemachineBrain cinemachineBrain;
    public Animator PlayerAnimator; 
    public Transform ExploradorOriginalPosition;
    public GameObject exploradorCurrentPosition;
    
    [Header("Player Audios")]
    public AudioSource _walkAudioSource;
    public AudioSource _runAudioSource;
    public  AudioSource _breathingNormalAudioSource;
    public  AudioSource _breathingRunAudioSource;
    
    

    [Header("Bike Scripts")] 
    public hoverCraft hoverCraft;
    public hoverCraftTilt hoverCraftTilt;
    public GameObject HoverBikeParent; 
    public GameObject bikeCamera;
    public SkinnedMeshRenderer PlayerBikeAnimation;
    public Rigidbody bikeRigidBody; 
    public Transform exitPositionRight;
    public ParticleSystem speedLinesParticles; 
    
    [Header("Bike Audios")]
    
    public AudioSource idleAudioSource;
    public AudioSource accelerationSound; 
    public  AudioSource thirdAudioSource;

    PhotonView PV;

    
    void Awake()

    {
        PV = GetComponent<PhotonView>();

    }

    private void Start()
    {
     
            StartCoroutine(AssignBikeRecourses());
       
    }

    IEnumerator AssignBikeRecourses()
    {
        yield return new WaitForSeconds(1f);
        hoverCraft = GameObject.Find("HoverBikeController").GetComponentInChildren<hoverCraft>();
        hoverCraftTilt = GameObject.Find("HoverBikeController").GetComponentInChildren<hoverCraftTilt>();
        HoverBikeParent = GameObject.Find("BikeMesh");
        bikeCamera = GameObject.Find("BikeCamera");
        bikeRigidBody = GameObject.Find("HoverBikeController").GetComponentInChildren<Rigidbody>();
        exitPositionRight = transform.Find("ExitPositionRight");
        PlayerBikeAnimation = GameObject.Find("ExploradorBikeMesh").GetComponent<SkinnedMeshRenderer>();
        exitPositionRight = GameObject.Find("ExitPositionRight").GetComponent<Transform>();
        speedLinesParticles = GameObject.Find("SpeedLinesParticleSystem").GetComponent<ParticleSystem>();
        idleAudioSource = GameObject.Find("IdleBikeSound").GetComponent<AudioSource>();
        accelerationSound = GameObject.Find("AccelerationBikeSound").GetComponent<AudioSource>();
        thirdAudioSource = GameObject.Find("FullAccelerationBikeSound").GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other) 
    { 
        if (other.gameObject.CompareTag("BikeUp"))
        {
            Debug.Log("Puede subirse a la moto ");
            caninteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BikeUp"))
        {
            Debug.Log("No puede subirse a la moto");
            caninteract = false; 
       
        }
    }

    
    void Update()
    {
        if(!PV.IsMine)
            return;
        if (Input.GetKeyDown(KeyCode.E) && (caninteract))
        {
            
            caninteract = false; 
            bikeRigidBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            fadeManager.DOFade(1f, 1f);
            exploradorAnimatorManager.enabled = false;
            exploradorPlayerController.enabled = false;
            ExploradorCameraManager.enabled = false;
            characterController.enabled = false;
            
            cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
            
            StartCoroutine(FadePlayerAudios());
            StartCoroutine(HoverBikeStart());
            
        } 
        
        if (Input.GetKeyDown(KeyCode.E) && (canDown))
        {
            canDown = false;  
            fadeManager.DOFade(1f, 1f);
            hoverCraft.enabled = false;
            hoverCraftTilt.enabled = false;
            bikeRigidBody.velocity = Vector3.zero;
            bikeRigidBody.angularVelocity = Vector3.zero;
            speedLinesParticles.Stop();
            
           
            
            StartCoroutine(HoverBikeDown());
            
            
        } 
    }
    
    private IEnumerator HoverBikeDown()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Bike AudiosFades");
  
        float fadeTime = 0.1f; // tiempo para desvanecer el sonido

    
        float startVolume = idleAudioSource.volume;
        float endVolume = 0f;
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            idleAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
            yield return null;
        }
        idleAudioSource.volume = endVolume;


        startVolume = accelerationSound.volume;
        endVolume = 0f;
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            accelerationSound.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
            yield return null;
        }
        accelerationSound.volume = endVolume;

        
        startVolume = thirdAudioSource.volume;
        endVolume = 0f;
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            thirdAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
            yield return null;
        }
        thirdAudioSource.volume = endVolume;
        PlayerCamera.transform.position = CameraTransform.position;
        PlayerCamera.transform.rotation = CameraTransform.rotation;
        bikeCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        PlayerBikeAnimation.enabled = false; 
        meshRenderer.enabled = true;
        PlayerController.transform.position = exitPositionRight.position;
        PlayerController.transform.rotation = exitPositionRight.rotation; 
        yield return new WaitForSeconds(0.2f);
        PlayerAnimator.SetBool("IsErect", false);
        exploradorAnimatorManager.enabled = true;
        exploradorPlayerController.enabled = true;
        ExploradorCameraManager.enabled = true;
        characterController.enabled = true;
        PlayerController.transform.parent = null;
        bikeRigidBody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        yield return new WaitForSeconds(0.5f);
        fadeManager.DOFade(0f, 1f);
        exploradorCurrentPosition.transform.position = ExploradorOriginalPosition.transform.position; 
        exploradorCurrentPosition.transform.rotation = ExploradorOriginalPosition.transform.rotation;
        PlayerCamera.transform.position = CameraTransform.position;
        PlayerCamera.transform.rotation = CameraTransform.rotation;




    }
        private IEnumerator FadePlayerAudios()
        {
            yield return new WaitForSeconds(0.1f);
            PlayerAnimator.SetBool("IsErect", true);
            Debug.Log("AudiosFades");
            if (!_breathingNormalAudioSource.isPlaying)
            {
                _breathingNormalAudioSource.Play();
            }
            float fadeTime = 0.1f; // tiempo para desvanecer el sonido

            // Desvanece el volumen del _breathingNormalAudioSource
            float startVolume = _breathingNormalAudioSource.volume;
            float endVolume = 0.2f;
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _breathingNormalAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _breathingNormalAudioSource.volume = endVolume;

            // Desvanece el volumen del _breathingRunAudioSource
            startVolume = _breathingRunAudioSource.volume;
            endVolume = 0f;
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _breathingRunAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _breathingRunAudioSource.volume = endVolume;

            // Desvanece el volumen del _walkAudioSource
            startVolume = _walkAudioSource.volume;
            endVolume = 0f;
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _walkAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _walkAudioSource.volume = endVolume;

            // Desvanece el volumen del _runAudioSource
            startVolume = _runAudioSource.volume;
            endVolume = 0f;
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _runAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _runAudioSource.volume = endVolume;
            
            yield return new WaitForSeconds(1f);
           
           
            
        }
        
     
        private IEnumerator HoverBikeStart()
        
        {
            yield return new WaitForSeconds(1f);
            PlayerBikeAnimation.enabled = true; 
            yield return new WaitForSeconds(1f);
            bikeRigidBody.constraints = RigidbodyConstraints.None; 
            Debug.Log("MeshHide");
            meshRenderer.enabled = false; 
            fadeManager.DOFade(0f, 1f);
            hoverCraft.enabled = true;
            hoverCraftTilt.enabled = true;
            bikeCamera.SetActive(true);
            PlayerCamera.SetActive(false);
            PlayerController.transform.parent = HoverBikeParent.transform; 
            yield return new WaitForSeconds(1f);
            canDown = true; 

        }
        
        
}
