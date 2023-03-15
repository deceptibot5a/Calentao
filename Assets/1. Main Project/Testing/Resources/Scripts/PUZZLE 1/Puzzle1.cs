using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Puzzle1 : MonoBehaviour
{
    public PasswordGenerator passwordGenerator;
    public TextMeshProUGUI passwordText;
    public Button[] digitButtons;

    private string password;
    private int digitsEntered;
    private float timer;

    void Start()
    {
        GeneratePassword();
        UpdatePasswordText();
        Debug.Log(password);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 12.0f)
        {
            GeneratePassword();
            UpdatePasswordText();
            digitsEntered = 0;
            timer = 0.0f;
        }
    }

    public void EnterDigit(int digit)
    {
        if (digitsEntered < 6)
        {
            password = password.Substring(0, digitsEntered) + digit.ToString() + password.Substring(digitsEntered + 1);
            digitsEntered++;
            UpdatePasswordText();
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
}