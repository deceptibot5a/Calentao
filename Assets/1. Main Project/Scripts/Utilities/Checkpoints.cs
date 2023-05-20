using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;


public class Checkpoints : MonoBehaviour
{
    public Transform checkpoint;
    public CanvasGroup deathPanel;
    public bool isDead;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            StartCoroutine(TurnTrue());
            if (isDead = true)
            {
                other.transform.position = checkpoint.position;
                isDead = false;
            }

            StartCoroutine(FadeInAndOut());
            Debug.Log("Toque el checkpoint");
        }
    }
    
    
    IEnumerator FadeInAndOut()
    {
        yield return new WaitForSeconds(0.01f);
        TurnOnDeathPanel();
        yield return new WaitForSeconds(1f);
        TurnOffDeathPanel();
        Debug.Log("si hice el fade");
    }

    public void TurnOnDeathPanel()
    {
    LeanTween.alphaCanvas(deathPanel, 1f, 1f).setOnComplete(TurnOffDeathPanel);
    }
    public void TurnOffDeathPanel()
    {
        LeanTween.alphaCanvas(deathPanel, 0f, 1f);
    }
    IEnumerator TurnTrue()
    {
        yield return new WaitForSeconds(6f);
        isDead = true;
        Debug.Log("isDead = true");
    }
    
    
}
