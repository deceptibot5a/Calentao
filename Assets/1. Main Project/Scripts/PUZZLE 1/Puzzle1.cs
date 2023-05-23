using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Photon.Pun;
using UnityEngine.Playables;


public class Puzzle1 : MonoBehaviourPunCallbacks
{
    public TMP_Text passwordText;
    public TMP_Text currentPassText;
    public GameObject door;
    public Slider sliderExplorador, sliderGuia;
    public Animator error;
    public Animator Correct;
    public GameObject correctobj;
    public GameObject completedDialogBox;
    private int digitsEntered;
    private float currentTime;
    private bool solved = false;
    

    [SerializeField] private InteractionsPlayer1 buttoncamera;
    [SerializeField] private string password;
    [SerializeField] private string currentPassword;
    [SerializeField] private float timer;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Image puzzleComplete, puzzleLocked;
    
    public PlayableDirector FinishChallenge; 
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
            timer = 30.0f;
        }

        sliderExplorador.value = timer / 30.0f;
        sliderGuia.value = timer / 30.0f;
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
                    photonView.RPC("ActivateFinishChallengeRPC", RpcTarget.All, null); // Llama al método RPC para activar el timeline
                    
                    //Poner aqui dialogo de que se completo el primer reto
                    StartCoroutine(MoveDialog());
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
    
    void ActivateFinishChallenge()
    {
        FinishChallenge.Play();
    }

    IEnumerator MoveDialog()
    {
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveX(completedDialogBox, 50, 0.5f).setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(8f);
        LeanTween.moveX(completedDialogBox, -700, 0.5f).setEase(LeanTweenType.easeInOutBack);
    }
    
    [PunRPC]
    void ActivateFinishChallengeRPC()
    {
        ActivateFinishChallenge();
    }
    
}