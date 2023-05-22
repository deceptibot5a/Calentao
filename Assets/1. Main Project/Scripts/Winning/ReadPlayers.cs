using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadPlayers : MonoBehaviour
{
    private string[] playersNames;
    private string[] playersTimes;
    private string[] playersRawTimes;
    private int[] rawTimes;

    private string playerPath;
    private string timesPath;
    private string rawTimesPath;

    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private Transform content;

    void Start() {
        playerPath = Application.dataPath + "/players.txt";
        timesPath = Application.dataPath + "/times.txt";
        rawTimesPath = Application.dataPath + "/rawTimes.txt";
    }

    public void UpdateArray() {
        playersNames = File.ReadAllLines(playerPath);
        playersTimes = File.ReadAllLines(timesPath);
        ConvertArray();

        for (int j = 0; j < playersNames.Length - 1; j++) {
            for (int i = 0; i < playersNames.Length - 1; i++) {
                if (rawTimes[i] > rawTimes[i + 1]) {

                    int tmpR = rawTimes[i];
                    string tmpP = playersRawTimes[i];
                    string tmpT = playersTimes[i];
                    string tmpN = playersNames[i];

                    rawTimes[i] = rawTimes[i + 1];
                    playersRawTimes[i] = playersRawTimes[i + 1];
                    playersTimes[i] = playersTimes[i + 1];
                    playersNames[i] = playersNames[i + 1];

                    rawTimes[i + 1] = tmpR;
                    playersRawTimes[i + 1] = tmpP;
                    playersTimes[i + 1] = tmpT;
                    playersNames[i + 1] = tmpN;
                }
            }
        }
    }
    private void ConvertArray() {
        playersRawTimes = File.ReadAllLines(rawTimesPath);
        rawTimes = new int[playersRawTimes.Length];
        
        for (int i = 0; i < playersRawTimes.Length; i++) {
            int.TryParse(playersRawTimes[i], out rawTimes[i]);
        }
    }
    public void DisplayNames() {
        for (int i = 0; i < playersNames.Length; i++) {
            TextMeshProUGUI newText = Instantiate(textPrefab);
            newText.transform.SetParent(content);
            newText.text = playersTimes[i] + "  -  " + playersNames[i];
        }
    }
}
