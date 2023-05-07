using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BikeUpController : MonoBehaviour
{
    public string lado; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Debug.Log("Jugador Entra " + lado);
      

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Debug.Log("Jugador sale " + lado);
  
        }
    }
}
