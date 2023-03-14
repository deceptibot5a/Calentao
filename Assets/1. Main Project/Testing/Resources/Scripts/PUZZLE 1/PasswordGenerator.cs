using UnityEngine;

public class PasswordGenerator : MonoBehaviour
{
    private string password;
    
    public void GeneratePassword()
    {
        password = "";

        for (int i = 0; i < 3; i++)
        {
            password += Random.Range(0, 10).ToString();
        }

        string[] figures = { "fig1", "fig2", "fig2" };
        for (int i = 0; i < 3; i++)
        {
            password += figures[Random.Range(0, figures.Length)];
        }
    }
    
    public string GetPassword()
    {
        return password;
    }
}