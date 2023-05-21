using System.Collections;
using System.Collections.Generic;
using Calentao.PlayerContol;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] ExploradorCameraManager playerCamera;
    [SerializeField] private ExploradorPlayerController playerControllerex;
    [SerializeField] ExploradorAnimatorManager playerAnimator;
    [SerializeField] Animator animator;
    [SerializeField] PhotonView photonView; 

    public GameObject pauseMenuPanel;
    public Button resumeButton;
    private bool isPaused = false;
    

    private void Start()
    {
        // Desactivar el menú de pausa al iniciar el juego
        pauseMenuPanel.SetActive(false);
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

    private void PauseGame()
    {
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerCamera.enabled = false;
        playerControllerex.enabled = false;
        playerAnimator.enabled = false;
        animator.SetBool("IsErect", true);
        pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.enabled = true;
        playerControllerex.enabled = true;
        playerAnimator.enabled = true;
        animator.SetBool("IsErect", false);
        pauseMenuPanel.SetActive(false);
    }
}