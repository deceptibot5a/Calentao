using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuManager_Test : MonoBehaviour
{
    public enum JugadoresMinimos
    {
        Minimo1Jugador = 1,
        Minimo2Jugador = 2
    }
    
    
    public static MenuManager_Test Instance;
    [SerializeField] private Menu_test[] menus;
    [SerializeField] private GameObject player1, player2;
    [SerializeField] private Button startButton;
    private bool player1Selected = false;
    private bool player2Selected = false;
    public JugadoresMinimos jugadoresMinimos;

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length == (int)jugadoresMinimos)
        {
            startButton.interactable = true;
        }
        else
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
        for(int i=0; i < menus.Length; i++)
        {
            if(menus[i].menuName == menuName)
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
        for(int i=0; i < menus.Length; i++)
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
    
    public void Player1Selected()
    {
        player1.GetComponent<Image>().color = Color.green;
        Destroy(player2);
        player1Selected = true;
        
    }
    public void Player2Selected()
    {
        player2.GetComponent<Image>().color = Color.green;
        Destroy(player1);
        player2Selected = true;
    }
    
}
