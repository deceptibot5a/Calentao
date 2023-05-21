using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private SaveFile saveFile;
    [SerializeField] private FileManager fileManager;
    //private string timeText;
    private float finishTime;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player1")) {
            WinTheGame();
        }
    }
    void WinTheGame() {
        Debug.Log("Ganamos");
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        finishTime = timer.timeRemaining;
        DisplayTime(finishTime);
    }

    void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        saveFile.timeData = ("Completado en: " + string.Format("{0:00}:{1:00}", minutes, seconds));
        fileManager.Save();
        //timeText = ("Completado en: " + string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
