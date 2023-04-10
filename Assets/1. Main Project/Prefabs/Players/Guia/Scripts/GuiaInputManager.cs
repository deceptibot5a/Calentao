using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Calentao.GuiaContol
{
public class GuiaInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput GuiaInput;
    
    public Vector2 GuiaMove {get; private set;}
    
    public Vector2 GuiaLook {get; private set;}
        
    public bool GuiaRun {get; private set;}
    
    private InputActionMap _currentMap;

    private InputAction _guiaMoveAction;
        
    private InputAction _guiaLookAction;

    private InputAction _guiaRunAction;
    private void Awake()
    {
        HideCursor();
        _currentMap = GuiaInput.currentActionMap;
        _guiaMoveAction = _currentMap.FindAction("GuiaMove");
        _guiaLookAction = _currentMap.FindAction("GuiaLook");
        _guiaRunAction = _currentMap.FindAction("GuiaRun");

        _guiaMoveAction.performed += guiaOnMove;
        _guiaLookAction.performed += guiaOnLook;
        _guiaRunAction.performed += onRun; 
            
        _guiaMoveAction.canceled += guiaOnMove;
        _guiaLookAction.canceled += guiaOnLook;
        _guiaRunAction.canceled += onRun; 
        
    }
    
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private  void guiaOnMove(InputAction.CallbackContext context)
    {
        GuiaMove = context.ReadValue<Vector2>();
    }
    private  void guiaOnLook(InputAction.CallbackContext context)
    {
        GuiaLook = context.ReadValue<Vector2>();
    }
    private  void onRun(InputAction.CallbackContext context)
    {
        GuiaRun = context.ReadValueAsButton(); 
    }
    private void OnEnable()
    {
        _currentMap.Enable();

    }

    private void OnDisable()
    {
        _currentMap.Disable();

    }

    }
}
