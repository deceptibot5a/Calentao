using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Calentao.PlayerContol
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;
        [SerializeField] private InteractionsPlayer1 buttoncamera;
        [SerializeField] private InteractionsPlayer2 buttoncamera2;
        [SerializeField] private InputActionReference interact, exitInteract;
        [SerializeField] private bool isplayer2;
        
        public  bool caninteract = false;
       
        public  bool isinpuzzle = false;
        public Vector2 Move {get; private set;}
        
        public Vector2 Look {get; private set;}
        
        public bool Run {get; private set;}

        private InputActionMap _currentMap;

        private InputAction _moveAction;
        
        private InputAction _lookAction;

        private InputAction _runAction;

        private void Awake()
        {
            
            HideCursor();
            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction = _currentMap.FindAction("Run");

            _moveAction.performed += onMove;
            _lookAction.performed += onLook;
            _runAction.performed += onRun; 
            
            _moveAction.canceled += onMove;
            _lookAction.canceled += onLook;
            _runAction.canceled += onRun;

            if (isplayer2)
            {
                buttoncamera2 = FindObjectOfType<InteractionsPlayer2>();
            }
            else
            {
                buttoncamera = FindObjectOfType<InteractionsPlayer1>();
            }
            
        }
        
        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        
        
        
        private  void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }
        private  void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }
        private  void onRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton(); 
        }

        private void OnEnable()
        {
            _currentMap.Enable();
            interact.action.performed += Interacting;
            exitInteract.action.performed += exitInteracting;
        }

        private void OnDisable()
        {
            _currentMap.Disable();
            interact.action.performed -= Interacting;
            exitInteract.action.performed -= exitInteracting;
        }

        private void Interacting(InputAction.CallbackContext obj)
        {
            if (caninteract)
            {
                if (isplayer2)
                {
                    buttoncamera2.interacted();
                }
                else
                {
                    buttoncamera.interacted();
                }
                
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
                if (isplayer2)
                {
                    buttoncamera2.stopInteraction();
                }
                else
                {
                    buttoncamera.stopInteraction();
                }
                
            }
            else
            {
                Debug.Log("no esta en un puzzle");
            }
        }
        
    }
}
