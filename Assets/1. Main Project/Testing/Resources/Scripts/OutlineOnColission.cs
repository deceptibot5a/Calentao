using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutlineOnColission : MonoBehaviour
{
    [SerializeField] private Outline objectWithOutline;

   private void OnTriggerEnter(Collider other)
   {
         if (other.CompareTag("Player"))
         {
              objectWithOutline.enabled = true;
         }
   }

   private void OnTriggerExit(Collider other)
   {
         if (other.CompareTag("Player"))
         {
              objectWithOutline.enabled = false;
         }
   }
}
