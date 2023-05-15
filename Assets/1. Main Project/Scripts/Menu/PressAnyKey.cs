using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyKey : MonoBehaviour
{
    public GameObject welcomePanel;
    public GameObject background;
    private void FixedUpdate()
    {
        if (Input.anyKey)
        {
            
            background.GetComponent<Image>().raycastTarget = false;
            welcomePanel.GetComponent<PressAnyKey>().enabled = false;
            LeanTween.alpha(welcomePanel.GetComponent<RectTransform>(), 0f, 0.3f).setOnComplete(DeactivateWelcomePanel);
        }
    }
    
    public void DeactivateWelcomePanel()
    {
        MenuManager_Test.Instance.ActivateBlackBackground();
        MenuManager_Test.Instance.OpenContract();
        welcomePanel.SetActive(false);
    }
}
