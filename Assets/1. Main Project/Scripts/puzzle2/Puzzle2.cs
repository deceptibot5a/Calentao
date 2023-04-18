using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Puzzle2 : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis, click;
   
    [SerializeField] private Camera cam2;
    [SerializeField] private PlayerInteractions ray;
    [SerializeField] private CinemachineVirtualCamera puzzlecamera;
    [SerializeField] private float speed = 1;
    [SerializeField] private bool inverted;
    
    private Vector2 rotation;
    private bool rotateAllowed;
    public static bool highlighted;
    private Transform cam;
    [SerializeField]  List<GameObject> objects = new List<GameObject>();
    [SerializeField]  int maxObjects = 3;

    private void Start()
    {
        StartCoroutine(SetRaycast());
    }

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
    
    IEnumerator SetRaycast()
    {
        yield return new WaitForSeconds(0.1f);
        ray = GameObject.FindWithTag("Player2").GetComponent<PlayerInteractions>();
    }
    
    public void Puzzle2Off()
    {
        ray.canray = false;
        pressed.Disable();
        axis.Disable();
    }
    
    

    public void AddObject(GameObject obj)
    {
        objects.Add(obj);

        if (objects.Count > maxObjects)
        {
            GameObject firstObject = objects[0];
            objects.RemoveAt(0);
            firstObject.SetActive(false);
        }
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
