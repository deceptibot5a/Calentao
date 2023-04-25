using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Puzzle2 : MonoBehaviour
{
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

    private bool puzzle2on = false;

    private void Start()
    {
        StartCoroutine(SetRaycast());
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && (puzzle2on))
        {
            StartCoroutine(Rotate());
            ray.clicked();
        }
        if (Input.GetMouseButtonUp(0)&& (puzzle2on))
        {
            rotateAllowed = false;
        }
    }

    public void Puzzle2On()
    {
        cam = puzzlecamera.transform;
        ray.canray = true;
        puzzle2on = true;

        rotation.x = Input.GetAxis("Horizontal");
        rotation.y = Input.GetAxis("Vertical");
    }
    
    IEnumerator SetRaycast()
    {
        yield return new WaitForSeconds(0.1f);
        ray = GameObject.FindWithTag("Player2").GetComponent<PlayerInteractions>();
    }
    
    public void Puzzle2Off()
    {
        ray.canray = false;
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
