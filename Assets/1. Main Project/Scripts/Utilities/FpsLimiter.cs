using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    public int targetFPS = 60; // Establece el FPS objetivo deseado

    void Awake()
    {
        Application.targetFrameRate = targetFPS; 
    }
}


