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
    
    [Header("Menu Settings")]
    [SerializeField] private Menu_test[] menus;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject createRoomButton;
    [SerializeField] private GameObject findRoomButton;
    [SerializeField] private GameObject blackBackground;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject contractImage;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject loadingBackground;
    [SerializeField] private GameObject findRoomsScreen;
    [SerializeField] private GameObject createRoomScreen;
    [SerializeField] private GameObject roomScreen;
    [SerializeField] private GameObject nombreJugador;
    [SerializeField] private GameObject exploradorButton, guiaButton;
    [SerializeField] private GameObject startGameButton;
    
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    /*
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu_test menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu_test menu)
    {
        menu.Close();
    }
*/

    public void OpenContract()
    {
        LeanTween.move(contractImage.GetComponent<RectTransform>(),new Vector3(0,0,0) , 0.3f).setEase(LeanTweenType.easeInSine);
        CloseLoadingScreen();
    }
    
    public void OpenMainMenu()
    {
        LeanTween.alphaCanvas(mainMenuScreen.GetComponent<CanvasGroup>(), 1f, 0.6f);
    }
    
    public void CloseMainMenu()
    {
        LeanTween.alphaCanvas(mainMenuScreen.GetComponent<CanvasGroup>(), 0f, 0.2f);
    }
    
    public void OpenFindRooms()
    { 
        LeanTween.alphaCanvas(findRoomsScreen.GetComponent<CanvasGroup>(), 1f, 0.3f).setOnComplete(CloseMainMenu);
    }
    public void CloseFindRooms()
    {
        LeanTween.alphaCanvas(findRoomsScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
    }
    
    public void OpenCreateRooms()
    {
        //HideButtons();
        CloseMainMenu();
        LeanTween.alphaCanvas(createRoomScreen.GetComponent<CanvasGroup>(), 1f, 0.3f);
    }
    public void CloseCreateRooms()
    {
        LeanTween.alphaCanvas(createRoomScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
    }

    public void OpenRoom()
    {
        CloseLoadingScreen();
        CloseCreateRooms();
        LeanTween.alphaCanvas(mainMenuScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(roomScreen.GetComponent<CanvasGroup>(), 1f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        LeanTween.alphaCanvas(nombreJugador.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
    }

    public void CloseRoom()
    {
        LeanTween.alphaCanvas(roomScreen.GetComponent<CanvasGroup>(), 0f, 0.3f).setEase(LeanTweenType.easeInOutBack);
    }
    
    
    public void CloseContract()
    {
        DeactiveBlackBackground();
        LeanTween.move(contractImage.GetComponent<RectTransform>(),new Vector3(0,1000,0) , 0.4f).setEase(LeanTweenType.easeInOutBack);
    }
    
    public void OpenLoadingScreen()
    {
        loadingBackground.GetComponent<Image>().raycastTarget = true;
        LeanTween.alphaCanvas(loadingScreen.GetComponent<CanvasGroup>(), 1f, 0.3f).setEase(LeanTweenType.easeInOutBack);
    }
    public void CloseLoadingScreen()
    {
        loadingBackground.GetComponent<Image>().raycastTarget = false;
        LeanTween.alphaCanvas(loadingScreen.GetComponent<CanvasGroup>(), 0f, 0.8f).setEase(LeanTweenType.easeInOutBack);
    }

    
    
    
    public void BackToMainMenu()
    {
        LeanTween.alphaCanvas(nombreJugador.GetComponent<CanvasGroup>(), 1f, 0.3f).setEase(LeanTweenType.easeInOutBack);
        OpenMainMenu();
        CloseRoom();
        CloseFindRooms();
        CloseContract();
        CloseCreateRooms();
    }


    

    
    public void DeactivateLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }
    public void ActivateBlackBackground()
    {
        LeanTween.alpha(blackBackground.GetComponent<RectTransform>(), 0.75f, 0.3f);
    }
    public void DeactiveBlackBackground()
    {
        blackBackground.GetComponent<Image>().raycastTarget = false;
        LeanTween.alpha(blackBackground.GetComponent<RectTransform>(), 0f, 0.3f);
    }
     
    
    

    
    
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
    
    public void CloseGame()
    {
        Application.Quit();
    }
}
