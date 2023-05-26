using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Puzzle2 : MonoBehaviour
{
    [SerializeField] private Player2Interactions ray;
    [SerializeField] private CinemachineVirtualCamera puzzlecamera; 
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField]  List<GameObject> objects = new List<GameObject>();
    [SerializeField]  int maxObjects = 3;
    [SerializeField] Material unpressedButtonMaterial;
    public static bool highlighted;

    private Quaternion startRotation;
    private Vector3 startMousePosition;
    private Transform cam;
    private Coroutine resetRotationCoroutine;
    private bool puzzle2on = false;

    private void Start()
    {
        StartCoroutine(SetRaycast());
        startRotation = transform.rotation;
    }
    
    public void Puzzle2On()
    {
        cam = puzzlecamera.transform;
        ray.canray = true;
        puzzle2on = true;
    }
    
    public void Puzzle2Off()
    {
        ray.canray = false;
        if (resetRotationCoroutine == null)
        { 
            resetRotationCoroutine = StartCoroutine(ResetRotation());
        }
    }
    
    private void OnMouseDown()
    {
        if (puzzle2on)
        {
            startMousePosition = Input.mousePosition;
        }
    }
    
    private void OnMouseDrag()
    {
        if (puzzle2on)
        {
            float deltaX = Input.GetAxis("Mouse X");
            Quaternion rotation = Quaternion.Euler(0f, deltaX * rotationSpeed, 0f);
            transform.rotation *= rotation;
        }
    }
    
    private IEnumerator ResetRotation()
    {
        float elapsedTime = 0f;
        float duration = 2f;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = startRotation;
        resetRotationCoroutine = null;
    }
    
    IEnumerator SetRaycast()
    {
        yield return new WaitForSeconds(0.1f);
        ray = GameObject.FindWithTag("Player2").GetComponent<Player2Interactions>();
    }

    public void AddObject(GameObject obj)
    {
        objects.Add(obj);

        if (objects.Count > maxObjects)
        {
            GameObject firstObject = objects[0];
            objects.RemoveAt(0);
            LeanTween.scale(firstObject, new Vector3(0f, 0f, 0f), 0.5f).setEaseOutSine();
            firstObject.GetComponent<PlatformDestroyer>().DeactivatePlatform();
        }
    }
    public void RemoveObject(GameObject plataforma)
    {
        objects.Remove(plataforma);
    }
    
    

}
