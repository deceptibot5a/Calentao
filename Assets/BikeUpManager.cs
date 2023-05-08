using System.Collections;
using System.Collections.Generic;
using AlekGames.HoverCraftSystem.Systems.Addons;
using AlekGames.HoverCraftSystem.Systems.Main;
using UnityEngine;
using DG.Tweening;
using Cinemachine; 

public class BikeUpManager : MonoBehaviour
{
    [Header("Player Scripts & References")]
    public bool caninteract = false;
    public CanvasGroup fadeManager;
    public ExploradorPlayerController exploradorPlayerController;
    public ExploradorAnimatorManager exploradorAnimatorManager;
    public ExploradorCameraManager ExploradorCameraManager;
    public CharacterController characterController;
    public SkinnedMeshRenderer meshRenderer;
    public GameObject PlayerController; 
    public GameObject PlayerCamera;
    public CinemachineBrain cinemachineBrain; 
    
    [Header("Audios")]
    public AudioSource _walkAudioSource;
    public AudioSource _runAudioSource;
    public  AudioSource _breathingNormalAudioSource;
    public  AudioSource _breathingRunAudioSource;

    [Header("Bike Scripts")] 
    public hoverCraft hoverCraft;
    public hoverCraftTilt hoverCraftTilt;
    public GameObject HoverBikeParent; 
    public GameObject bikeCamera; 
   
    private void OnTriggerEnter(Collider other) 
    { 
        if (other.gameObject.CompareTag("BikeUp"))
        {
            Debug.Log("hola");
            caninteract = true; 
            

        }
    }
    

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (caninteract))
        {
            caninteract = false; 
            fadeManager.DOFade(1f, 1f);
            exploradorAnimatorManager.enabled = false;
            exploradorPlayerController.enabled = false;
            ExploradorCameraManager.enabled = false;
            characterController.enabled = false;
            cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
            
            StartCoroutine(FadeAudios());
            StartCoroutine(HoverBikeStart());
            
        } 
    }
        private IEnumerator FadeAudios()
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("AudiosFades");
            if (!_breathingNormalAudioSource.isPlaying)
            {
                _breathingNormalAudioSource.Play();
            }
            float fadeTime = 0.5f; // tiempo para desvanecer el sonido

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
            Debug.Log("AudiosFades");
            
        }
        
        private IEnumerator HoverBikeStart()
        
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("MeshHide");
            meshRenderer.enabled = false; 
            fadeManager.DOFade(0f, 1f);
            hoverCraft.enabled = true;
            hoverCraftTilt.enabled = true;
            bikeCamera.SetActive(true);
            PlayerCamera.SetActive(false);
            PlayerController.transform.parent = HoverBikeParent.transform; 

        }
        
        
}
