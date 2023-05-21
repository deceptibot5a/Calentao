using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxRotation : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
    }
}