using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager_Test : MonoBehaviour
{
    public static MenuManager_Test Instance;
    
    [SerializeField] private Menu_test[] menus;
    [SerializeField] private GameObject player1, player2;
    

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
    }
    public void Player2Selected()
    {
        player2.GetComponent<Image>().color = Color.green;
        Destroy(player1);
    }
    
}
