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
    }
    
    
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text =( "Tiempo restante:" + string.Format("{0:00}:{1:00}", minutes, seconds));
    }
    
    IEnumerator BacktoMenu()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}