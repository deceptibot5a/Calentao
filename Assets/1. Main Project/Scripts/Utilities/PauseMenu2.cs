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
    public Image pauseMenuBackground;
    public GameObject pauseMenuContainer;
    public GameObject fotoJugador;
    private bool isPaused = false;
    private bool assigned;
    public Button resumeButton;
    public PhotonView photonView;
    
    public AudioSource UI_Sounds;
    public AudioClip pauseSounds;
    public AudioSource[] audioSources;
    
    private void Start()
    {
        if (!photonView.IsMine)
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

    public void PauseGame()
    {
        Debug.Log("Debería sonar"); 
        UI_Sounds.PlayOneShot(pauseSounds);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = 0f;
        }
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerCamera2.enabled = false;
        playerControllerex2.enabled = false;
        playerAnimator2.enabled = false;
        animator2.SetBool("IsErect", true);
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
        playerCamera2.enabled = true;
        playerControllerex2.enabled = true;
        playerAnimator2.enabled = true;
        animator2.SetBool("IsErect", false);
        LeanTween.alphaCanvas(pauseMenuBackground.GetComponent<CanvasGroup>(), 0f, 0.3f);
        LeanTween.moveLocal(pauseMenuContainer, new Vector3(124.5f,1018,-2), 0.3f).setEaseInSine();
        LeanTween.moveX(fotoJugador, -560, 0.3f).setEaseInSine().setEaseInSine().setOnComplete(TurnOffPause);

    }
    
    void TurnOffPause()
    {
        pauseMenuUI.SetActive(false);
    }

}