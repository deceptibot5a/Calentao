using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class PlayerNameSaver : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject playerTime;
    [SerializeField] private TMP_InputField txtInput;
    [SerializeField] private ReadPlayers readPlayers;
    private string playerName;
    private string playerPath;

    void Start() {
        playerPath = Application.dataPath + "/players.txt";
    }

    public void SaveName() {
        playerName = txtInput.text;

        AppendText(playerName, playerPath);
        ShowLeaderboard();
    }

    void ShowLeaderboard() {
        playerTime.SetActive(false);
        readPlayers.UpdateArray();
        leaderboard.SetActive(true);
        readPlayers.DisplayNames();
    }

    public void GoBackToMain() {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void AppendText(string txtInput, string appendFile) {
        if (!File.Exists(appendFile)) {
            File.WriteAllText(appendFile, txtInput);
        } else {
            using (var writer = new StreamWriter(appendFile, true)) {
                writer.WriteLine(txtInput);
            }
        }
    }
}
