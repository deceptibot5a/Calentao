using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using WebSocketSharp;

public class MenuManager_Test : MonoBehaviour
{
    public PhotonView photonView;
    
    public enum JugadoresMinimos
    {
        Minimo1Jugador = 1,
        Minimo2Jugador = 2
    }

    
    [Header("Players Settings")]
    public JugadoresMinimos jugadoresMinimos; 
    [SerializeField] private GameObject player1, player2;
    [SerializeField] Button player1Button, player2Button;
    public int playersSelected = 0;
    private bool playersReadyToStart = false;
    private bool player1Selected = false;
    private bool player2Selected = false;
    public static MenuManager_Test Instance;
    public bool isLoading = false;
    
    
    [Header("Menu Settings")]
    [SerializeField] private Menu_test[] menus;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject createRoomButton;
    [SerializeField] private GameObject findRoomButton;
    [SerializeField] private GameObject blackBackground;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] public GameObject loadingPlanetImage;
    [SerializeField] private GameObject contractImage;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject loadingBackground;
    [SerializeField] private GameObject findRoomsScreen;
    [SerializeField] private GameObject createRoomScreen;
    [SerializeField] private GameObject roomScreen;
    [SerializeField] private GameObject nombreJugador;
    [SerializeField] private GameObject exploradorButton, guiaButton;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject brilloInferior;
    [SerializeField] private GameObject panelGuia,panelExplorador;
    
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        OpenMainMenu();
        roomScreen.SetActive(false);
        findRoomsScreen.SetActive(false);
        createRoomScreen.SetActive(false);
    }
    
    private void Update()
    {
        if (playersSelected == (int)jugadoresMinimos)
        {
           LeanTween.move(startGameButton.GetComponent<RectTransform>(),new Vector3(0,0,0) , 0.3f).setEase(LeanTweenType.easeInOutBack);
        }
        else
        {
            LeanTween.move(startGameButton.GetComponent<RectTransform>(),new Vector3(0,-400,0) , 0.3f).setEase(LeanTweenType.easeInOutBack);
        }
        
    }

    private void Awake()
    {
        Instance = this;
    }

    #region Contract screen

    public void OpenContract()
    {
        LeanTween.move(contractImage.GetComponent<RectTransform>(),new Vector3(0,0,0) , 0.3f).setEase(LeanTweenType.easeInSine);
        CloseLoadingScreen();
    }
    public void CloseContract()
    {
        DeactiveBlackBackground();
        LeanTween.move(contractImage.GetComponent<RectTransform>(),new Vector3(0,1000,0) , 0.4f).setEase(LeanTweenType.easeInOutBack);
    }

    #endregion
    
    #region Main Menu

    public void OpenMainMenu()
    {
        LeanTween.alphaCanvas(mainMenuScreen.GetComponent<CanvasGroup>(), 1f, 0.3f);
        LeanTween.moveLocalY(mainMenuScreen, 8f, 0.4f);
        mainMenuScreen.GetComponent<CanvasGroup>().interactable = true;
    }
    
    public void CloseMainMenu()
    {
        LeanTween.alphaCanvas(mainMenuScreen.GetComponent<CanvasGroup>(), 0f, 0.2f);
        LeanTween.moveLocalY(mainMenuScreen, -20f, 0.4f);
        mainMenuScreen.GetComponent<CanvasGroup>().interactable = false;
    }
    public void BackToMainMenu()
    {
        LeanTween.alphaCanvas(nombreJugador.GetComponent<CanvasGroup>(), 1f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(brilloInferior.GetComponent<CanvasGroup>(), 1f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        OpenMainMenu();
        CloseRoom();
        CloseFindRooms();
        CloseContract();
        CloseCreateRooms();
    }

    #endregion
    
    #region Create rooms
    public void OpenCreateRooms()
    {
        //HideButtons();
        CloseMainMenu();
        LeanTween.alphaCanvas(createRoomScreen.GetComponent<CanvasGroup>(), 1f, 0.3f);
        LeanTween.moveLocalY(createRoomScreen, 10f, 0.4f);
        createRoomScreen.GetComponent<CanvasGroup>().interactable = true;
        createRoomScreen.SetActive(true);
        
    }
    public void CloseCreateRooms()
    {
        LeanTween.alphaCanvas(createRoomScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.moveLocalY(createRoomScreen, -20f, 0.4f).setOnComplete(TurnOffCreateRoom);
    }
    
    public void TurnOffCreateRoom()
    {
        createRoomScreen.SetActive(false);
    }
    #endregion
    
    #region Find rooms 

    public void OpenFindRooms()
    { 
        LeanTween.alphaCanvas(findRoomsScreen.GetComponent<CanvasGroup>(), 1f, 0.3f);
        LeanTween.moveLocalY(findRoomsScreen, 10f, 0.4f);
        CloseMainMenu();
        findRoomsScreen.GetComponent<CanvasGroup>().interactable = true;
        findRoomsScreen.SetActive(true);
    }
    public void CloseFindRooms()
    {
        LeanTween.alphaCanvas(findRoomsScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.moveLocalY(findRoomsScreen, -20f, 0.4f).setOnComplete(TurnOffFindRoom);
    }
    public void TurnOffFindRoom()
    {
        findRoomsScreen.SetActive(false);
    }

    #endregion

    #region Room Functions

    public void OpenRoom()
    {
        CloseLoadingScreen();
        CloseCreateRooms();
        LeanTween.moveLocalX(panelGuia, -680f, 0.6f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.moveLocalX(panelExplorador, 680f, 0.6f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(brilloInferior.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(mainMenuScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(roomScreen.GetComponent<CanvasGroup>(), 1f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(nombreJugador.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        
        roomScreen.SetActive(true);
        
    }

    public void CloseRoom()
    {
        LeanTween.moveLocalX(panelGuia, 1280f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.moveLocalX(panelExplorador, -1280f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(roomScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack).setOnComplete(TurnOffRoom);
    }
    
    public void TurnOffRoom()
    {
        roomScreen.SetActive(false);
    }

    #endregion

    #region Loading screen

    public void OpenLoadingScreen()
    {
        loadingBackground.GetComponent<Image>().raycastTarget = true;
        LeanTween.alphaCanvas(loadingScreen.GetComponent<CanvasGroup>(), 1f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        
        isLoading = true;
    }
    public void CloseLoadingScreen()
    {
        loadingBackground.GetComponent<Image>().raycastTarget = false;
        LeanTween.alphaCanvas(loadingScreen.GetComponent<CanvasGroup>(), 0f, 0.8f).setEase(LeanTweenType.easeInOutBack);
        //LeanTween.moveX(loadingPlanetImage.GetComponent<RectTransform>(), 850f, 0.8f).setEaseInOutSine();
        isLoading = false;
    }

    #endregion
    
    #region Background Activators

    public void ActivateBlackBackground()
    {
        LeanTween.alpha(blackBackground.GetComponent<RectTransform>(), 0.75f, 0.3f);
    }
    public void DeactiveBlackBackground()
    {
        blackBackground.GetComponent<Image>().raycastTarget = false;
        LeanTween.alpha(blackBackground.GetComponent<RectTransform>(), 0f, 0.3f);
    }
    public void DeactivateGameObject(GameObject gameObjectToTurnOff)
    {
        gameObjectToTurnOff.SetActive(false);
    }

    #endregion

    
    public void CloseGame()
    {
        Application.Quit();
    }
    #region Player selection logic
    [PunRPC]
    public void Player1Selected()
    {
        player1.GetComponent<Image>().color = Color.green;
        player1Selected = true;
        playersSelected++;
        photonView.RPC("UpdatePlayersSelected", RpcTarget.All, playersSelected);
        player1Button.interactable = false;
        player2Button.interactable = false;
        photonView.RPC("DisablePlayer1", RpcTarget.All);
        player2Button.onClick.RemoveAllListeners();
    }
    
    [PunRPC]
    public void UpdatePlayersSelected(int newPlayersSelected)
    {
        playersSelected = newPlayersSelected;
    }

    [PunRPC]
    public void Player2Selected()
    {
       
            player2.GetComponent<Image>().color = Color.green;
            player2Selected = true;
            playersSelected++;
            photonView.RPC("UpdatePlayersSelected", RpcTarget.All, playersSelected);
            player1Button.interactable = false;
            player2Button.interactable = false;
            photonView.RPC("DisablePlayer2", RpcTarget.All);
            player1Button.onClick.RemoveAllListeners();
        
    }

    [PunRPC]
    public void ResetSelection()
    {
        if (player1Selected == true)
        {
            player1Selected = false;
            player1Button.interactable = true;
            player2Button.interactable = false;
            player1.GetComponent<Image>().color = Color.white;
            photonView.RPC("UpdatePlayersSelected", RpcTarget.All, playersSelected);
            playersSelected --;
            
        }
        else if (player2Selected == true)
        {
            player2Selected = false;
            player2Button.interactable = true;
            player1Button.interactable = false;
            player2.GetComponent<Image>().color = Color.white;
            photonView.RPC("UpdatePlayersSelected", RpcTarget.All, playersSelected);
            playersSelected --;
        }
    }
    
    [PunRPC]
    public void DisablePlayer1()
    {
        player1Button.interactable = false;
    }
    [PunRPC]
    public void DisablePlayer2()
    {
        player2Button.interactable = false;
    }
    #endregion
}
