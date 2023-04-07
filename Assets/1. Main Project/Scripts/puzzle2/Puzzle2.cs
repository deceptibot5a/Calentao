using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Puzzle2 : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis, click;
    private Transform cam;
    [SerializeField] private Camera cam2;
    public PlayerInteractions ray;
    [SerializeField] private CinemachineVirtualCamera puzzlecamera;
    [SerializeField] private float speed = 1;
    private Vector2 rotation;
    private bool rotateAllowed;
    [SerializeField] private bool inverted;
    public static bool highlighted;

    public void Puzzle2On()
    {
        cam = puzzlecamera.transform;
        cam2 = Camera.main;
        ray.canray = true;
        pressed.Enable();
        axis.Enable();
        click.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        click.performed += ray.clicked;
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }
    
    public void Puzzle2Off()
    {
        ray.canray = false;
        pressed.Disable();
        axis.Disable();
    }
    

    private IEnumerator Rotate()
    {
        if (!highlighted)
        {
            rotateAllowed = true;
            while (rotateAllowed)
            {
                rotation *= speed;
                transform.Rotate(Vector3.up * (inverted? 1: -1), rotation.x, Space.World); 
            
                //rotacion completa en 3D
                //transform.Rotate(cam.right * (inverted? -1: 1), rotation.y, Space.World);
                yield return null;
            }
        }

    }

}
