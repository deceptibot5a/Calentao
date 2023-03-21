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
        
        public bool caninteract = false;
        [SerializeField] private InteractionsPlayer1 buttoncamera;

        public bool isinpuzzle = false;
        
        
        [SerializeField] private InputActionReference interact, exitInteract;

        [SerializeField] private float AnimBlendSpeed = 8.9f;

        [SerializeField] private Transform CameraRoot; 
        
        [SerializeField] private Transform Camera;
        
        [SerializeField] private  float UpperLimit = -40f;

        [SerializeField] private float BottomLimit = 70f; 
        
        [SerializeField] private float MouseSensitivity = 21.9f; 
        
        [SerializeField] private GameObject cam;
        [SerializeField] private GameObject virtualCam;
        
        
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
            
            _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0 ,zVelDifference)), ForceMode.VelocityChange);
            
            _animator.SetFloat(_xVelHash , _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);

        }
        
        
        private void OnEnable()
        {
            interact.action.performed += Interacting;
            exitInteract.action.performed += exitInteracting;

        }

        private void OnDisable()
        {
            interact.action.performed -= Interacting;
            exitInteract.action.performed -= exitInteracting;

        }

        private void Interacting(InputAction.CallbackContext obj)
        {
            if (caninteract)
            {
                buttoncamera.interacted();
            }
            else
            {
                Debug.Log("no hay interacciones");
            }
        }
        
        private void exitInteracting(InputAction.CallbackContext obj)
        {
            if (isinpuzzle)
            {
                buttoncamera.stopInteraction();
            }
            else
            {
                Debug.Log("no esta en un puzzle");
            }
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
