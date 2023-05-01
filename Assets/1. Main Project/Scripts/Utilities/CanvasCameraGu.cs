using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CanvasCameraGu : MonoBehaviour
{

    public Canvas cavascamera;
    void Start()
    {
        StartCoroutine(CanvAsassign());
    }

    IEnumerator CanvAsassign()
    {
        yield return new WaitForSeconds(0.1f);
        cavascamera.worldCamera = GameObject.Find("GuiaCinemachineBrain").GetComponent<Camera>();
    }

}
