using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Puzzle1 : MonoBehaviour
{
    public PasswordGenerator passwordGenerator;
    public TMP_Text passwordText;
    public TMP_Text currentPassText;
    public Button[] digitButtons;
    public GameObject door;
    public Slider Slider;
    public Animator error;
    public Animator Correct;
    public GameObject correctobj;

    [SerializeField] private InteractionsPlayer1 buttoncamera;
    
    [SerializeField]
    private string password;
    [SerializeField]
    private string currentPassword;
    private int digitsEntered;
    [SerializeField]
    private float timer;

    private float currentTime;

    private bool solved = false;

    void Start()
    {
        GeneratePassword();
        UpdatePasswordText();
        Debug.Log(password);
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
            GeneratePassword();
            UpdatePasswordText();
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
        passwordGenerator.GeneratePassword();
        password = passwordGenerator.GetPassword();
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