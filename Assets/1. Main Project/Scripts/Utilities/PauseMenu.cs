using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Photon.Pun;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI, tutorialImage, backButton;
    private bool isPaused = false;
    
    

    void Start()
    {
        pauseMenuUI.SetActive(false);
        tutorialImage.SetActive(false);
        backButton.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void ShowTutorial()
    {
      tutorialImage.SetActive(true);
        backButton.SetActive(true);
       
    }
    
    public void HideTutorial()
    {
        tutorialImage.SetActive(false);
        backButton.SetActive(false);
    }
    
        
    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void LoadScene(string sceneName)
    {
        PhotonNetwork.LeaveRoom(); // Para desconectar de la sala actual antes de cargar una nueva escena
        SceneManager.LoadScene(sceneName);
    }
}
