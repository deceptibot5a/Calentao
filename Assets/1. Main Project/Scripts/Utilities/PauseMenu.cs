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
    [SerializeField] ExploradorCameraManager playerCamera;
    [SerializeField] private ExploradorPlayerController playerControllerex;
    [SerializeField] ExploradorAnimatorManager playerAnimator;
    [SerializeField] Animator animator;




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
        playerControllerex = GameObject.FindWithTag("Player1").GetComponent<ExploradorPlayerController>();
        playerCamera = GameObject.Find("CM vcam1 explorador").GetComponent<ExploradorCameraManager>();
        playerAnimator = GameObject.Find("ExploradorFINALv1").GetComponent<ExploradorAnimatorManager>();
        animator = GameObject.Find("ExploradorFINALv1").GetComponent<Animator>();

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
        playerCamera.enabled = false;
        playerControllerex.enabled = false;
        playerAnimator.enabled = false;
        animator.SetBool("IsErect", true);

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
        playerCamera.enabled = true;
        playerControllerex.enabled = true;
        playerAnimator.enabled = true;
        animator.SetBool("IsErect", false);

    }

    public void LoadScene(string sceneName)
    {
        PhotonNetwork.LeaveRoom(); // Para desconectar de la sala actual antes de cargar una nueva escena
        SceneManager.LoadScene(sceneName);
    }
    

}
