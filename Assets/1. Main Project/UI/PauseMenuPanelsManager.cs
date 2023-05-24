using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuPanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject mainPanel;
    
    public void ShowPanel()
    {
        LeanTween.alphaCanvas(panel.GetComponent<CanvasGroup>(), 1f, 0.3f);
        panel.SetActive(true);
        LeanTween.alphaCanvas(mainPanel.GetComponent<CanvasGroup>(), 0f, 0.3f).setOnComplete(TurnOffMainPanel);
    }
    
    public void ClosePanel()
    {
        LeanTween.alphaCanvas(panel.GetComponent<CanvasGroup>(), 0f, 0.3f).setOnComplete(TurnOffPanel);
        mainPanel.SetActive(true);
        LeanTween.alphaCanvas(mainPanel.GetComponent<CanvasGroup>(), 1f, 0.3f);
    }
    
    void TurnOffPanel()
    {
        panel.SetActive(false);
    }
    void TurnOffMainPanel()
    {
        mainPanel.SetActive(false);
    }
}
