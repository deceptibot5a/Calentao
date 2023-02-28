using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigerrEnter : MonoBehaviour
{
    //When the player enters the trigger, destroy the object with this script
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
