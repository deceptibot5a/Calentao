using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] private GameObject[] projectionCams;

    public void CameraCheck(int index) {
        for (int i = 0; i < projectionCams.Length; i++) {
            if (index != i) projectionCams[i].SetActive(false);
            else projectionCams[i].SetActive(true);
        }
    }
}
