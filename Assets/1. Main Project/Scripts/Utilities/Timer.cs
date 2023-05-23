using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 600f;
    public Text timeText;
    public GameObject losePanel;
    private bool hasFinished = false;
    public bool garbanzoMessage = false;
    public GameObject audioBox;
    public float audioTime = 6f;
    public AudioSource audioGarbanzo;

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
            losePanel.SetActive(true);
            StartCoroutine(BacktoMenu());
            hasFinished = true;
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
    
    IEnumerator BacktoMenu()
    {
        yield return new WaitForSeconds(5);
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
    
}