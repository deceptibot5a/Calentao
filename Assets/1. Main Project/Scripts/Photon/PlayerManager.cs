using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public string playerPrefabName;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    

    GameObject controller;

    int kills;
    int deaths;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
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
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerPrefabName), Vector3.zero,Quaternion.identity);
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