using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OpenDoor : MonoBehaviour
{
    public PlayableDirector DoorOpen; 
    public PlayableDirector DoorClose;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            DoorOpen.Play();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            DoorClose.Play();
        }
    }
}
