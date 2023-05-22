using System.IO;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class CinematicEnded : MonoBehaviour
{
    [SerializeField] VideoPlayer cinematic;
    [SerializeField] private FadeToBlack fading;
    [SerializeField] private GameObject playerTime;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    private string filePath;

    void Start () {
        cinematic.loopPointReached += EndCinematic;
        filePath = Application.dataPath + "/overwrite.txt";
    }

    private void EndCinematic(VideoPlayer vp) {
        fading.fadeIn = true;
        fading.firstTime = false;
        StartCoroutine(ShowTime());
    }

    IEnumerator ShowTime() {
        yield return new WaitForSeconds(2);

        ReadFile();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ReadFile() {
        using (StreamReader reader = new StreamReader(filePath)) {
            string fileContent = reader.ReadToEnd();
            finalTimeText.text = fileContent;
            playerTime.SetActive(true);
        }
    }
    
}

