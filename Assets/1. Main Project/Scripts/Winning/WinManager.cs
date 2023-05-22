using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private SaveTime saveTime;
    
    private float finishTime;
    private string dataToSave;

    private string timesPath;
    private string rawTimesPath;

    void Start() {
        timesPath = Application.dataPath + "/times.txt";
        rawTimesPath = Application.dataPath + "/rawTimes.txt";
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player1")) {
            WinTheGame();
        }
    }
    void WinTheGame() {
        finishTime = 605 - timer.timeRemaining;
        DisplayTime(finishTime);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    void DisplayTime(float timeToDisplay) {
        int temp = (int)timeToDisplay;
        dataToSave = temp.ToString();
        saveTime.AppendText(dataToSave, rawTimesPath);

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        dataToSave = string.Format("{0:00}:{1:00}", minutes, seconds);
        saveTime.AppendText(dataToSave, timesPath);

        dataToSave = ("Felicidades, completaron la misión!\nTiempo: " + string.Format("{0:00}:{1:00}", minutes, seconds));
        saveTime.OverwriteText(dataToSave);
    }

}
