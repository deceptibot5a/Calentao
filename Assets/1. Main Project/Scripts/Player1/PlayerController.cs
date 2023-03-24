using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

namespace Calentao.PlayerContol
{
    public class PlayerController : MonoBehaviour
    {
        
        
        [SerializeField] private float AnimBlendSpeed = 8.9f;

        [SerializeField] private Transform CameraRoot; 
        
        [SerializeField] private Transform Camera;
        
        [SerializeField] private  float UpperLimit = -40f;

        [SerializeField] private float BottomLimit = 70f; 
        
        [SerializeField] private float MouseSensitivity = 21.9f; 
        
        [SerializeField] private GameObject cam;
        [SerializeField] private GameObject virtualCam;

        [SerializeField] private AudioSource _walkAudioSource;
        [SerializeField] private AudioSource _runAudioSource;
        [SerializeField] private AudioSource _breathingNormalAudioSource;
        [SerializeField] private AudioSource _breathingRunAudioSource;
        [SerializeField] private float AudioFadeSpeed = 1f;
        [SerializeField] private float BreathFadeSpeed = 0.2f;
        private float _breathingVolume = 0f;
        
        
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;

        private Animator _animator;

        private bool _hasAnimator;

        private int _xVelHash;
        private int _yVelHash;

        private float _xRotation; 

        private const float _walkSpeed = 2f; 
        
        private const float _runSpeed = 6f;

        private Vector2 _currentVelocity; 
        
        PhotonView PV;
       
        
        
        private void Awake()
        {
            PV = GetComponent<PhotonView>();
           
        }
        
        
        void Start()
        {

            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity"); 
            if (!PV.IsMine)
            {
                //Destroy(GetComponentInChildren<Camera>().gameObject);
                //cam.SetActive(false);
                virtualCam.SetActive(false);
                //Destroy(_playerRigidbody);
            }
        }

        private void Update()
        {
            if(!PV.IsMine)
                return;
        }

        private void FixedUpdate()
        {
            if(!PV.IsMine)
                return;
            Move();
        }

        private void LateUpdate()
        {

            CamMovements();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
            if (_inputManager.Move == Vector2.zero) targetSpeed = 0.1f;

            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

            var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
            var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;

            _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);

            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
            
            
            if (_currentVelocity.magnitude >= 0f && _currentVelocity.magnitude < 4f)
            {
                if (!_breathingNormalAudioSource.isPlaying)
                {
                   _breathingNormalAudioSource.Play();
                }
                _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 1f, BreathFadeSpeed * Time.deltaTime);
                _breathingRunAudioSource.volume = Mathf.Lerp(_breathingRunAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
            }
            else if (_currentVelocity.magnitude >= 4f)
            {
                if (!_breathingRunAudioSource.isPlaying)
                {
                    _breathingRunAudioSource.Play();
                }
                _breathingRunAudioSource.volume = Mathf.Lerp(_breathingRunAudioSource.volume, 1f, BreathFadeSpeed * Time.deltaTime);
                _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
            }
            else
            {
                _breathingNormalAudioSource.volume = Mathf.Lerp(_breathingNormalAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
                _breathingRunAudioSource.volume = Mathf.Lerp(_breathingRunAudioSource.volume, 0f, BreathFadeSpeed * Time.deltaTime);
            }

            if (_currentVelocity.magnitude > 0.1f && _currentVelocity.magnitude < 4f)
            {
                if (!_walkAudioSource.isPlaying)
                {
                    _walkAudioSource.Play();
                }
                _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, 1f, AudioFadeSpeed * Time.deltaTime);
                _runAudioSource.volume = Mathf.Lerp(_runAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            }
            else if (_currentVelocity.magnitude >= 4f)
            {
                if (!_runAudioSource.isPlaying)
                {
                    _runAudioSource.Play();
                }
                _runAudioSource.volume = Mathf.Lerp(_runAudioSource.volume, 1f, AudioFadeSpeed * Time.deltaTime);
                _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            }
            else
            {
                _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
                _runAudioSource.volume = Mathf.Lerp(_runAudioSource.volume, 0f, AudioFadeSpeed * Time.deltaTime);
            }
        }
        
        private IEnumerator FadeIn(AudioSource source, AudioClip clip, float fadeTime = 0.1f)
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                source.volume = Mathf.Lerp(0f, 1f, t / fadeTime);
                yield return null;
            }

            source.clip = clip;
            source.loop = true;
            source.Play();
        }

        private IEnumerator FadeOut(AudioSource source, float fadeTime = 0.1f)
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                source.volume = Mathf.Lerp(1f, 0f, t / fadeTime);
                yield return null;
            }

            source.Stop();
        }
        
        private void CamMovements()
        {
            if(!_hasAnimator) return;

            var Mouse_X = _inputManager.Look.x;
            var Mouse_y = _inputManager.Look.y;
            Camera.position = CameraRoot.position;

            _xRotation -= Mouse_y * MouseSensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            transform.Rotate(Vector3.up, Mouse_X * MouseSensitivity * Time.deltaTime);
        }
    }
}
