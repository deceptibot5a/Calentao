using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;

namespace Calentao.GuiaContol
{ 
    public class GuiaController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        
        private Rigidbody _playerRigidbody;
        private GuiaInputManager _guiaInputManager;

        private Animator _animator;

        private bool _hasAnimator;

        private int _xVelHash;
        private int _yVelHash;
        
        private const float _walkSpeed = 2f; 
        
        private const float _runSpeed = 6f;
        
        private Vector2 _currentVelocity; 
        
        void Start()
        {

            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _guiaInputManager = GetComponent<GuiaInputManager>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity"); 
      
        }

        private void FixedUpdate()
        {
            GuiaMove();
        }

        private void GuiaMove()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _guiaInputManager.GuiaRun ? _runSpeed : _walkSpeed;
            if (_guiaInputManager.GuiaMove == Vector2.zero) targetSpeed = 0.1f;

            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _guiaInputManager.GuiaMove.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _guiaInputManager.GuiaMove.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

            
            var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
            var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;
            
            _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);
            
            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
            
        }

    }
}
