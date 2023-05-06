using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PuzzleInteractions : MonoBehaviour
{

    public CinemachineVirtualCamera puzzlecam;

    public int PuzzleNumber;

    public InteractionsPlayer1 interactions;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PhotonView>().IsMine) 
        {
            Debug.Log("la camara ha sido asignada");
            interactions.uiInteraction();
            interactions.activecamera = puzzlecam;
            interactions.puzzletype = PuzzleNumber;

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Debug.Log("no hay camaras asignadas");
            interactions.uiInteractionOff();
            interactions.activecamera = null;
            interactions.puzzletype = 0;
        }
    }

}