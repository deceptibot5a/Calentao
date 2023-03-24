using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Photon.Pun;

public class Puzzle1 : MonoBehaviourPunCallbacks
{
    public TMP_Text passwordText;
    public TMP_Text currentPassText;
    public Button[] digitButtons;
    public GameObject door;
    public Slider Slider;
    public Animator error;
    public Animator Correct;
    public GameObject correctobj;
    private int digitsEntered;
    private float currentTime;
    private bool solved = false;

    [SerializeField] private InteractionsPlayer1 buttoncamera;
    [SerializeField] private string password;
    [SerializeField] private string currentPassword;
    [SerializeField] private float timer;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Image puzzleComplete, puzzleLocked;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GeneratePassword();
            photonView.RPC("SendPassword", RpcTarget.All, password);
            Debug.Log(password);
        }
    }

    [PunRPC]
    public void SendPassword(string password)
    {
        this.password = password;
        UpdatePasswordText();
    }
    [PunRPC]
    public void CompletePuzzle1()
    {   
        puzzleLocked.enabled = false;
        puzzleComplete.color = new Color(255,255,255,1);
    }

    void Update()
    {
        if (solved)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GeneratePassword();
                photonView.RPC("SendPassword", RpcTarget.All, password);
            }
            currentPassword = "";
            digitsEntered = 0;
            timer = 12.0f;
        }

        Slider.value = timer / 12.0f;
        UpdateCurrentPasswordText();
    }

    public void EnterDigit(int digit)
    {
        if (digitsEntered < 6)
        {
            if (digitsEntered == currentPassword.Length)
            {
                currentPassword += digit.ToString();
            }
            else
            {
                currentPassword = currentPassword.Substring(0, digitsEntered) + digit.ToString() + currentPassword.Substring(digitsEntered + 1);
            }
            digitsEntered++;

            if (digitsEntered == 6)
            {
                if (currentPassword == password)
                {
                    solved = true;
                    door.SetActive(false);
                    correctobj.SetActive(true);
                    buttoncamera.correct = true;
                    buttoncamera.stopInteraction();
                    Correct.SetTrigger("Correct");
                    photonView.RPC("CompletePuzzle1", RpcTarget.All, null);
                }
                else
                {
                    currentPassword = "";
                    digitsEntered = 0;
                    error.SetTrigger("Error");
                }
            }
        }
    }

    private void GeneratePassword()
    {
        password = "";

        for (int i = 0; i < 6; i++)
        {
            password += Random.Range(1, 10).ToString();
        }
    }

    private void UpdatePasswordText()
    {
        passwordText.text = password;
    }

    private void UpdateCurrentPasswordText()
    {
        currentPassText.text = currentPassword;
    }
}