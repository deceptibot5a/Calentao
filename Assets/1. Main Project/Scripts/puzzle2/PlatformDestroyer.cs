using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private Puzzle2Button puzzle2Button;
     public void DeactivatePlatform()
      {
          puzzle2Button.DeactivatePlatform();
      }

     private void OnTriggerEnter(Collider other)
     {
            if (other.CompareTag("Pez"))
            {
                puzzle2Button._instance.DeactivatePlatform();
                Debug.Log("plataforma desactivada");
            }
     }
}
