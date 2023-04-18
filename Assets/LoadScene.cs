using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string Scenename; 
    void Start()
    {
        PhotonNetwork.LoadLevel("MainMenu");
        PhotonNetwork.LeaveRoom();
    }
    
}
