using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject controller;
    public Transform player1Spawnpoint;
    public Transform player2Spawnpoint;
    
    public string playerPrefabName;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        player1Spawnpoint = GameObject.Find("SpawnPlayer1").transform;
        player2Spawnpoint = GameObject.Find("SpawnPlayer2").transform;
    }

    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        if (playerPrefabName == "PlayerController") // Si es el jugador 1
        {
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerPrefabName), player1Spawnpoint.position, player1Spawnpoint.rotation);
        }
        else if (playerPrefabName == "PlayerController2") // Si es el jugador 2
        {
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerPrefabName), player2Spawnpoint.position, player2Spawnpoint.rotation);
        }
    }
    
    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
    }
    
    public void SelectPlayer1()
    {
        playerPrefabName = "PlayerController";
       
    }
    
    public void SelectPlayer2()
    {
        playerPrefabName = "PlayerController2";
        
    }
    
}