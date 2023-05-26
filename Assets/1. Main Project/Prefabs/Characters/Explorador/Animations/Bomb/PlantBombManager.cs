using System.Collections;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class PlantBombManager : MonoBehaviour
{
    public GameObject virtualCam; 
    public Animator animator;
    public bool planting;
    public bool canPlant = false; 
    public bool bomb1 = false;
    public bool bomb2 = false;
    public ExploradorPlayerController exploradorPlayerController;
    public ExploradorAnimatorManager exploradorAnimatorManager;
    public ExploradorCameraManager ExploradorCameraManager;
    public CharacterController characterController;
    
    public CanvasGroup interact; 

    public GameObject cameraMain;
    public GameObject bombCamera;


    public CinemachineBrain cinemachineBrain;
    public Transform ExploradorOriginalPosition;
    public GameObject exploradorCurrentPosition; 
    
    [Header("Player Audios")]
    public AudioSource _walkAudioSource;
    public AudioSource _runAudioSource;
    public  AudioSource _breathingNormalAudioSource;
    public  AudioSource _breathingRunAudioSource;

    public GameObject Bomb01GameObject;
    public GameObject Bomb02GameObject;
    public GameObject bombReferencePosition;
    public GameObject handParent;
    
    PhotonView PV;
    
    
    //que es todo este espacio desperdiciado por dios
    



    private void Start()
    {
        if (!PV.IsMine)
        {
            virtualCam.SetActive(false);

        }
    }
    
    private void OnTriggerEnter(Collider other) 
    { 
        if (other.gameObject.CompareTag("PlantArea"))
        {
            Debug.Log("Puede plantar area 1");
            interact.alpha = 1f; 
            canPlant = true; 
            bomb1 = true;
            bomb2 = false; 

        }
        
        if  (other.gameObject.CompareTag("PlantArea2"))
        {
            interact.alpha = 1f; 
            Debug.Log("Puede plantar area 2");
            canPlant = true; 
            bomb1 = false; 
            bomb2 = true; 


        }
    }
    
    
    void Awake()

    {
        PV = GetComponent<PhotonView>();
        

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
            interact.alpha = 0f; 
            canPlant = false;
            planting = false;
            bomb1 = false;
            bomb2 = false; 

        }
        
        if (other.gameObject.CompareTag("PlantArea2"))
        {
            Debug.Log("No puede plantar");
            interact.alpha = 0f; 
            canPlant = false;
            planting = false;
            bomb1 = false;
            bomb2 = false; 

        }
    }
    void Update()
    {
        if(!PV.IsMine)
            return;
        if (canPlant)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!planting)
                {
                    interact.alpha = 0f; 
                    Debug.Log("Empezó a plantar");
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
        if (bomb1)
        {
        cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseOut;
        Bomb01GameObject.transform.parent = handParent.transform; 
        Bomb01GameObject.SetActive(false);
        StartCoroutine(FadePlayerAudios());
        Bomb01GameObject.transform.position = bombReferencePosition.transform.position; 
        Bomb01GameObject.transform.rotation = bombReferencePosition.transform.rotation; 
        cameraMain.SetActive(false);
        bombCamera.SetActive(true);
        planting = true;
        exploradorAnimatorManager.enabled = false;
        exploradorPlayerController.enabled = false;
        ExploradorCameraManager.enabled = false;
        animator.SetBool("Planting", true);
        }
        
        if (bomb2)
        {
            cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseOut;
            Bomb02GameObject.transform.parent = handParent.transform; 
            Bomb02GameObject.SetActive(false);
            StartCoroutine(FadePlayerAudios());
            Bomb02GameObject.transform.position = bombReferencePosition.transform.position; 
            Bomb02GameObject.transform.rotation = bombReferencePosition.transform.rotation; 
            cameraMain.SetActive(false);
            bombCamera.SetActive(true);
            planting = true;
            exploradorAnimatorManager.enabled = false;
            exploradorPlayerController.enabled = false;
            ExploradorCameraManager.enabled = false;
            animator.SetBool("Planting", true);
        }
        
    }
    
    private void FixedUpdate()
    {
        if(!PV.IsMine)
            return;
  
    }


    void CancelPlanting()
    {
        
        if (canPlant)
        {
            interact.alpha = 1f; 
        }
        if (bomb1)
        {
        Bomb01GameObject.transform.parent = handParent.transform; 
        Bomb01GameObject.SetActive(false);
        cameraMain.SetActive(true);
        bombCamera.SetActive(false);
        planting = false;
        exploradorAnimatorManager.enabled = true;
        exploradorPlayerController.enabled = true;
        ExploradorCameraManager.enabled = true;
        animator.SetBool("Planting", false);
        StartCoroutine(ExploradorOriginalPositionCorrutine()); 
        }
        
        if (bomb2)
        {
            Bomb02GameObject.transform.parent = handParent.transform; 
            Bomb02GameObject.SetActive(false);
            cameraMain.SetActive(true);
            bombCamera.SetActive(false);
            planting = false;
            exploradorAnimatorManager.enabled = true;
            exploradorPlayerController.enabled = true;
            ExploradorCameraManager.enabled = true;
            animator.SetBool("Planting", false);
            StartCoroutine(ExploradorOriginalPositionCorrutine()); 
        }
       
    }
    
    private IEnumerator ExploradorOriginalPositionCorrutine()
    {

        yield return new WaitForSeconds(0.5f); 
        Debug.Log("Position Original");
        exploradorCurrentPosition.transform.position = ExploradorOriginalPosition.transform.position; 
        exploradorCurrentPosition.transform.rotation = ExploradorOriginalPosition.transform.rotation;

    }
    
}