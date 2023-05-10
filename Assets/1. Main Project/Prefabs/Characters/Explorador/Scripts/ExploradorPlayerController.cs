using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class ExploradorPlayerController : MonoBehaviour

{

    CharacterController CharacterController;
    [SerializeField] private GameObject virtualCam;
    [SerializeField] private GameObject cinemachineBrain;

    public bool CanControl;
    public bool UseGravity;
    public bool CantWalk;
    public bool CanRun;

    public float WalkSpeed;
    public float RunSpeed;
    public float Speed;
    public float Gravity;

    private Vector3 MoveDirection = Vector3.zero;
    private Vector3 InAirMoveDirection = Vector3.zero;

    public bool IsFalling = false;
    public float FallHeightThreshold;
    
    [SerializeField] private AudioSource _walkAudioSource;
    [SerializeField] private AudioSource _runAudioSource;
    [SerializeField] private AudioSource _breathingNormalAudioSource;
    [SerializeField] private AudioSource _breathingRunAudioSource;
    [SerializeField] private float AudioFadeSpeed = 1f;
    [SerializeField] private float BreathFadeSpeed = 0.2f;
    private float _breathingVolume = 0f;
    public AudioClip _metalwalkAudio;
    public AudioClip _metalRunAudio; 
    public AudioClip _GrasswalkAudio;
    public AudioClip _GrassRunAudio; 
    
    PhotonView PV;
    
    


void Awake()

    {

        CharacterController = GetComponent<CharacterController>();
        PV = GetComponent<PhotonView>();


    }

private void Start()
{
    if (!PV.IsMine)
    {
        virtualCam.SetActive(false);
        cinemachineBrain.SetActive(false);

    }
}


void Update()

    {
        if(!PV.IsMine)
            return;
        GuiaAudioManager();
        if (CharacterController.isGrounded == true)
        {
            if (CanControl == true)
            {
                MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                MoveDirection = transform.TransformDirection(MoveDirection);

                if (MoveDirection.magnitude == 0) // Si el jugador no está moviendo
                {
                    Speed = 0;
                }   
                else // Si el jugador está moviéndose
                {
                    Speed = Input.GetKey(KeyCode.LeftShift) && CanRun && !CantWalk ? RunSpeed : WalkSpeed;
                    
                }

                MoveDirection *= Speed;
            }
            IsFalling = false;
        }
        else
        {
            if (CharacterController.velocity.y < -FallHeightThreshold)
            {
                IsFalling = true;
            }
        }

        if (UseGravity == true)
        {
            ApplyGravity();
        }

        CharacterController.Move(MoveDirection * Time.deltaTime);
    }
    
    private void GuiaAudioManager()
    { 
        if (IsFalling == false) 
        { 
            if (Speed >= 0f && Speed < 4f) 
            {
                if (!_breathingNormalAudioSource.isPlaying)
                {
                    _breathingNormalAudioSource.Play();
                }
                _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 0.2f, BreathFadeSpeed * Time.deltaTime);
                _breathingRunAudioSource.volume = Mathf.Lerp(_breathingRunAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
            }
            else if (Speed >= 4f) 
            { 
                if (!_breathingRunAudioSource.isPlaying)
            {
                _breathingRunAudioSource.Play();
            }
                _breathingRunAudioSource.volume = Mathf.Lerp(_breathingRunAudioSource.volume, 0.2f, BreathFadeSpeed * Time.deltaTime);
                 _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
            }
            else
            { 
                _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
                _breathingRunAudioSource.volume = Mathf.Lerp(_breathingRunAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
            }

            if (Speed > 0.1f && Speed < 4f)
            {
            if (!_walkAudioSource.isPlaying)
            {
                _walkAudioSource.Play();
            }
            _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, 0.3f, AudioFadeSpeed * Time.deltaTime);
            _runAudioSource.volume = Mathf.Lerp(_runAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            }
            else if (Speed >= 4f)
            {
            if (!_runAudioSource.isPlaying)
            {
                _runAudioSource.Play();
            }
            _runAudioSource.volume = Mathf.Lerp(_runAudioSource.volume, 0.3f, AudioFadeSpeed * Time.deltaTime);
            _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            }
            else
            {
            _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            _runAudioSource.volume = Mathf.Lerp(_runAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (IsFalling == true)
            {
                IsFalling = true;
                _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
                _runAudioSource.volume = Mathf.Lerp(_runAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            }
        }
    }
    
    private void FixedUpdate()
    {
        if(!PV.IsMine)
            return;
  
    }
    
    public void ApplyGravity()

    {

        MoveDirection.y -= Gravity * Time.deltaTime;

    }

}