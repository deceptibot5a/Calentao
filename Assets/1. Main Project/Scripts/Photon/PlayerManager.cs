using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;


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
        if (playerPrefabName == "PlayerController") StartCoroutine(CameraCinematica());
        if (playerPrefabName == "PlayerController2") StartCoroutine(CameraCinematica2());
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
    
    IEnumerator CameraCinematica()
    {
        yield return new WaitUntil(() => CameraPlayerAssing.instance != null); 
        CameraPlayerAssing.instance.EnableCamera(); 
    }
    
    IEnumerator CameraCinematica2()
    {
        yield return new WaitUntil(() => CameraPlayerAssing2.instance != null); 
        CameraPlayerAssing2.instance.EnableCamera(); 
    }
    
}