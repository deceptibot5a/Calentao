using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] GameObject inputfield, acceptButton;
    [SerializeField] GameObject welcomePanel;
    [SerializeField] TMP_Text welcomeText;
    
    
    
    public void SetPlayerName()
    {
        PhotonNetwork.NickName = playerNameInputField.text;
        welcomePanel.SetActive(true);
        welcomeText.text = "Preparate para la aventura," + PhotonNetwork.NickName + "!";
        Destroy(inputfield);
        Destroy(acceptButton);
    }
}
