using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Checkpoints : MonoBehaviour
{
    public Transform checkpoint;
    public CanvasGroup deathPanel;
    public float fadeTime = 0.3f;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            TurnOnDeathPanel();
            other.GetComponent<Transform>().position = checkpoint.position;
        }
    }

    public void TurnOnDeathPanel()
    {
    LeanTween.alphaCanvas(deathPanel, 1f, 0.3f).setOnComplete(TurnOffDeathPanel);
    }
    public void TurnOffDeathPanel()
    {
        LeanTween.alphaCanvas(deathPanel, 0f, 0.6f);
    }
    
}
