using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuManager_Test : MonoBehaviour
{
    public PhotonView photonView;
    
    public enum JugadoresMinimos
    {
        Minimo1Jugador = 1,
        Minimo2Jugador = 2
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public int playersSelected = 0;
    
    public static MenuManager_Test Instance;
    [SerializeField] private Menu_test[] menus;
    [SerializeField] private GameObject player1, player2;
    [SerializeField] private Button startButton;
    private bool player1Selected = false;
    private bool player2Selected = false;
    [SerializeField] private bool playersReadyToStart = false;
    public JugadoresMinimos jugadoresMinimos;
    [SerializeField] Button player1Button, player2Button;

    private void Update()
    {
        if (playersSelected == (int)jugadoresMinimos)
        {
            playersReadyToStart = true;
        }
        else
        {
            playersReadyToStart = false;
        }
        
        if (playersReadyToStart == true)
        {
            startButton.interactable = true;
        }
        else if (playersReadyToStart == false)
        {
            startButton.interactable = false;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

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

    [PunRPC]
    public void Player1Selected()
    {
        
        player1.GetComponent<Image>().color = Color.green;
        //Destroy(player2);
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
        //Destroy(player1);
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
