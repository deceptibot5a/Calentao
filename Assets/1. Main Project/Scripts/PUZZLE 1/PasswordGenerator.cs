using UnityEngine;

public class PasswordGenerator : MonoBehaviour
{
    private string password;
    
    public void GeneratePassword()
    {
        password = "";

        for (int i = 0; i < 6; i++)
        {
            password += Random.Range(1, 10).ToString();
        }
    }
    
    public string GetPassword()
    {
        return password;
    }
}