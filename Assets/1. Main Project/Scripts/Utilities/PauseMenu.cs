using System.Collections;
using System.Collections.Generic;
using Calentao.PlayerContol;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI, tutorialImage, backButton;
    private bool isPaused = false;
    private bool assigned;
   // private GuiaPlayerController guiaPlayerController;
   // private ExploradorPlayerController exploradorPlayerController; 

    void Start()
    {
        pauseMenuUI.SetActive(false);
        tutorialImage.SetActive(false);
        backButton.SetActive(false);
        StartCoroutine(AssignPlayersControllers());
    }
    
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Escape))
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
    
    IEnumerator AssignPlayersControllers()
    {
        yield return new WaitForSeconds(3f); // Espera 2 segundos
        //playerController1 = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
        //_playerController2 = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
    }
}
