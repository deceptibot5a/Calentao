using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 600f;
    public Text timeText;
    public GameObject timerPanel;
    private bool hasFinished = false;
    public bool garbanzoMessage = false;
    public GameObject audioBox;
    public float audioTime = 6f;
    public AudioSource audioGarbanzo;
    public static Timer instance;
    public GameObject player1, player2;
    public GameObject allScene;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(AssignPlayers());
    }


    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        
        
        else if (!hasFinished)
        {
            timeRemaining = 0;
            DisplayTime(timeRemaining);
            timerPanel.SetActive(false);
            SceneManager.LoadScene("LooseCinematic", LoadSceneMode.Additive);
            hasFinished = true;
            player1.SetActive(false);
            player2.SetActive(false);
            allScene.SetActive(false);
        }

        if (timeRemaining <= 60 && garbanzoMessage == false)
        {
            StartCoroutine(moveAudioDialog());
            garbanzoMessage = true;
            audioGarbanzo.Play();
            Debug.Log("Garbanzo");
        }
    }
    
    
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text =( string.Format("{0:00}:{1:00}", minutes, seconds));
    }
    
    void BackToMainMenu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    IEnumerator moveAudioDialog()
    {
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveX(audioBox, 60, 0.5f).setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(audioTime);
        LeanTween.moveX(audioBox, -700, 0.5f).setEase(LeanTweenType.easeInOutBack);
    }
    
    public void StopTimer()
    {
        StopAllCoroutines(); 
        hasFinished = true;
    }
    IEnumerator AssignPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        
    }
}