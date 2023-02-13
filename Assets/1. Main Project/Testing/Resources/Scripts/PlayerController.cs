using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calentao.PlayerContol
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f; 
        
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;

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
            _inputManager = GetComponent<InputManager>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity"); 
        }

        private void FixedUpdate()
        {
            Move();
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
    }
}
