using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Unity.VisualScripting;

public class PuzzleInteractions : MonoBehaviour
{

    public CinemachineVirtualCamera puzzlecam;
    
    public CinemachineBrain cinemachineBrain;
    
    public int PuzzleNumber;

    public InteractionsPlayer1 interactions;

    private void Start()
    {
        AssingPlayer();
    }

    IEnumerator AssingPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        cinemachineBrain = GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<CinemachineBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PhotonView>().IsMine) 
        {
            Debug.Log("la camara ha sido asignada");
            interactions.uiInteraction();
            interactions.activecamera = puzzlecam;
            interactions.puzzletype = PuzzleNumber;
            cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseOut;
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
