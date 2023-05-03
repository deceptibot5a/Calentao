using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangers : MonoBehaviour
{
   public Material nuevoMaterial; 
   public Renderer renderer;
   
       private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player1"))
           {
               renderer.material = nuevoMaterial;
           }
       }
}
