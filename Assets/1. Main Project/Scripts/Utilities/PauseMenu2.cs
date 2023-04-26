using System.Collections;
using System.Collections.Generic;
using Calentao.PlayerContol;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class PauseMenu2 : MonoBehaviour
{
    public GameObject pauseMenuUI, tutorialImage, backButton;
    private bool isPaused = false;
    private bool assigned;
    [SerializeField] GuiaCameraManager playerCamera2;
    [SerializeField] private GuiaPlayerController playerControllerex2;
    [SerializeField] GuiaAnimationManager playerAnimator2;
    [SerializeField] Animator animator2;

     
    

    void Start()
    {
        StartCoroutine(PauseInstance());
        pauseMenuUI.SetActive(false);
        tutorialImage.SetActive(false);
        backButton.SetActive(false);

  
    }
    
    IEnumerator PauseInstance()
    {
        yield return new WaitForSeconds(0.1f);
        playerControllerex2 = GameObject.FindWithTag("Player2").GetComponent<GuiaPlayerController>();
        playerCamera2 = GameObject.Find("CM vcam1 Guia").GetComponent<GuiaCameraManager>();
        playerAnimator2 = GameObject.Find("GuiaMesh").GetComponent<GuiaAnimationManager>();
        animator2 = GameObject.Find("GuiaMesh").GetComponent<Animator>();

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
        playerCamera2.enabled = false;
        playerControllerex2.enabled = false;
        playerAnimator2.enabled = false;
        animator2.SetBool("IsErect", true);

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
        playerCamera2.enabled = true;
        playerControllerex2.enabled = true;
        playerAnimator2.enabled = true;
        animator2.SetBool("IsErect", false);

    }

    public void LoadScene(string sceneName)
    {
        PhotonNetwork.LeaveRoom(); // Para desconectar de la sala actual antes de cargar una nueva escena
        SceneManager.LoadScene(sceneName);
    }
    

}
