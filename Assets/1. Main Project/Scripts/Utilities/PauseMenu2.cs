using System.Collections;
using System.Collections.Generic;
using Calentao.PlayerContol;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class PauseMenu2 : MonoBehaviour
{
    
    [SerializeField] GuiaCameraManager playerCamera2;
    [SerializeField] private GuiaPlayerController playerControllerex2;
    [SerializeField] GuiaAnimationManager playerAnimator2;
    [SerializeField] Animator animator2;
    
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    private bool assigned;
    public Button resumeButton;
    public PhotonView photonView;
    
    private void Start()
    {
        // Desactivar el menú de pausa al iniciar el juego
        pauseMenuUI.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected && photonView.IsMine)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
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
    
        
    public void ResumeGame()
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

}