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
    public Image pauseMenuBackground;
    public GameObject pauseMenuContainer;
    public GameObject fotoJugador;
    public Button resumeButton;
    public AudioSource UI_Sounds;
    public AudioClip pauseSounds; 
    private bool isPaused = false;

    public AudioSource[] audioSources; 

    private PhotonView PV; 

    private void Start()
    {
        if (!PV.IsMine)
        {
            UI_Sounds.enabled = false; 

        }
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
        Debug.Log("Debería sonar"); 
        UI_Sounds.PlayOneShot(pauseSounds);
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerCamera.enabled = false;
        playerControllerex.enabled = false;
        playerAnimator.enabled = false;
        
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = 0f;
        }
        animator.SetBool("IsErect", true);
        pauseMenuPanel.SetActive(true);
        LeanTween.alphaCanvas(pauseMenuBackground.GetComponent<CanvasGroup>(), 1f, 0.3f);
        LeanTween.moveLocal(pauseMenuContainer, new Vector3(124.5f,-3,-2), 0.3f).setEaseInSine();
        LeanTween.moveLocalX(fotoJugador, -960, 0.3f).setEaseInSine();
    }

    public void ResumeGame()
    {
        UI_Sounds.PlayOneShot(pauseSounds);
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.enabled = true;
        playerControllerex.enabled = true;
        playerAnimator.enabled = true;
        animator.SetBool("IsErect", false);
        LeanTween.alphaCanvas(pauseMenuBackground.GetComponent<CanvasGroup>(), 0f, 0.3f);
        LeanTween.moveLocal(pauseMenuContainer, new Vector3(124.5f,1018,-2), 0.3f).setEaseInSine();
        LeanTween.moveX(fotoJugador, -560, 0.3f).setEaseInSine().setEaseInSine().setOnComplete(TurnOffPause);
    }

    public void GoToMainMenu()
    {
        UI_Sounds.PlayOneShot(pauseSounds);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu_Oficial");
    }
   
    void TurnOffPause()
    {
        pauseMenuPanel.SetActive(false);
    }
    
    IEnumerator TurnOffPauseMenu()
    {
        yield return new WaitForSeconds(0.2f);
        pauseMenuPanel.SetActive(false);
    }
}