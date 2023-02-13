using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayer : MonoBehaviour
{
    public void LoadPlayerOne() {
        SceneManager.LoadScene("Player1");
    }
    public void LoadPlayerTwo() {
        SceneManager.LoadScene("Player2");
    }
}
