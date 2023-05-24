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
        MenuManager_Test.Instance.CloseContract();
        LeanTween.move(welcomePanel.GetComponent<RectTransform>(),new Vector3(-515,440,0), 0.3f).setEase(LeanTweenType.easeInOutBack);
        welcomeText.text = "Bienvenido al sistema, " + PhotonNetwork.NickName + "!";
    }
}
