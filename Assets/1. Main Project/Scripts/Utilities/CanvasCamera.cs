using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CanvasCamera : MonoBehaviour
{

    public Canvas cavascamera;
    void Start()
    {
        cavascamera.worldCamera = GameObject.Find("ExpCinemachineBrain").GetComponent<Camera>();
    }

}
