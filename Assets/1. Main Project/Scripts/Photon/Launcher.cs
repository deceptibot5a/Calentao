using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;


public class Launcher : MonoBehaviourPunCallbacks
{
	public static Launcher Instance;

	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject playerListItemPrefab;
	[SerializeField] GameObject startGameButton;
	[SerializeField] GameObject initialRoom;
	private static Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
		PhotonNetwork.ConnectToRegion(("us"));
		PhotonNetwork.AutomaticallySyncScene = true;
		
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		PhotonNetwork.JoinLobby();
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnJoinedLobby()
	{
		//MenuManager_Test.Instance.OpenMenu("WelcomeMenu");
		MenuManager_Test.Instance.CloseLoadingScreen();
		Debug.Log("Joined Lobby");
	}

	public void CreateRoom()
	{
		if (string.IsNullOrEmpty(roomNameInputField.text))
		{
			return;
		}

		RoomOptions options = new RoomOptions();
		options.MaxPlayers = 2;

		PhotonNetwork.CreateRoom(roomNameInputField.text, options);
		MenuManager_Test.Instance.OpenLoadingScreen();
		
	}

	public override void OnJoinedRoom()
	{
		
		//MenuManager_Test.Instance.OpenFindRooms(); AQUI VA ES EL CUARTO DONDE SE ESCOGEN PERSONAJESSSSSS
		roomNameText.text = PhotonNetwork.CurrentRoom.Name;

		Player[] players = PhotonNetwork.PlayerList;

		foreach(Transform child in playerListContent)
		{
			Destroy(child.gameObject);
		}

		for(int i = 0; i < players.Count(); i++)
		{
			Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}

		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		Debug.LogError("Room Creation Failed: " + message);
		//MenuManager_Test.Instance.OpenMenu("Error");
	}

	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager_Test.Instance.OpenLoadingScreen();
	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		//MenuManager_Test.Instance.OpenMenu("Loading");
	}

	public override void OnLeftRoom()
	{
		//MenuManager_Test.Instance.OpenMenu("Title");
		cachedRoomList.Clear();
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		
		foreach (Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
		}

		for (int i = 0; i < roomList.Count; i++)
		{
			RoomInfo info = roomList[i];
			if (info.RemovedFromList)
			{
				cachedRoomList.Remove(info.Name);
			}
			else
			{
				cachedRoomList[info.Name] = info;
			}
		}

		foreach (KeyValuePair<string, RoomInfo> entry in cachedRoomList)
		{
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(cachedRoomList[entry.Key]);
		}
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room is full";
		Debug.LogError("Join Room Failed: " + message);
		//MenuManager_Test.Instance.OpenMenu("Error");
	}
	
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}
}