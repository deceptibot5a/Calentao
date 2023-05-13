using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBombManager : MonoBehaviour
{
    public Animator animator;
    public bool planting;
    public bool canPlant = false; 
    public ExploradorPlayerController exploradorPlayerController;
    public ExploradorAnimatorManager exploradorAnimatorManager;
    public ExploradorCameraManager ExploradorCameraManager;
    public CharacterController characterController;

    public GameObject cameraMain;
    public GameObject bombCamera;


    public Transform ExploradorOriginalPosition;
    public GameObject exploradorCurrentPosition; 
    
    [Header("Player Audios")]
    public AudioSource _walkAudioSource;
    public AudioSource _runAudioSource;
    public  AudioSource _breathingNormalAudioSource;
    public  AudioSource _breathingRunAudioSource;
    
    
    private void OnTriggerEnter(Collider other) 
    { 
        if (other.gameObject.CompareTag("PlantArea"))
        {
            Debug.Log("Puede plantar ");
            canPlant = true; 

        }
    }
    
    private IEnumerator FadePlayerAudios()
        {
            yield return new WaitForSeconds(0.1f);
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlantArea"))
        {
            Debug.Log("No puede plantar");
            canPlant = false;
            planting = false; 

        }
    }
    void Update()
    {
        if (canPlant)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!planting)
                {
                    StartPlanting();
                }
            }
            else if (planting)
            {
                CancelPlanting();
            }
        }
       
    }

    void StartPlanting()
    {
        StartCoroutine(FadePlayerAudios());

        cameraMain.SetActive(false);
        bombCamera.SetActive(true);
        planting = true;
        exploradorAnimatorManager.enabled = false;
        exploradorPlayerController.enabled = false;
        ExploradorCameraManager.enabled = false;
        animator.SetBool("Planting", true);
      
    }

    void CancelPlanting()
    {
        
        cameraMain.SetActive(true);
        bombCamera.SetActive(false);
        planting = false;
        exploradorAnimatorManager.enabled = true;
        exploradorPlayerController.enabled = true;
        ExploradorCameraManager.enabled = true;
       
        
        animator.SetBool("Planting", false);
        StartCoroutine(ExploradorOriginalPositionCorrutine()); 
        // Aquí puedes agregar el código para detener la lógica de la bomba
    }
    
    private IEnumerator ExploradorOriginalPositionCorrutine()
    {

        yield return new WaitForSeconds(0.5f); 
        Debug.Log("Position Original");
        exploradorCurrentPosition.transform.position = ExploradorOriginalPosition.transform.position; 
        exploradorCurrentPosition.transform.rotation = ExploradorOriginalPosition.transform.rotation;

    }
    
}