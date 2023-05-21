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
    public float TPdelayTime = 0.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            Invoke("TeleportPlayer", TPdelayTime);

            StartCoroutine(FadeInAndOut());
            Debug.Log("Toque el checkpoint");
        }
    }
    
    
    IEnumerator FadeInAndOut()
    {
        TurnOnDeathPanel();
        yield return new WaitForSeconds(0.8f);
        TurnOffDeathPanel();
        Debug.Log("si hice el fade");
    }

    public void TurnOnDeathPanel()
    {
        LeanTween.alphaCanvas(deathPanel, 1f, 0.3f);
    }
    public void TurnOffDeathPanel()
    {
        LeanTween.alphaCanvas(deathPanel, 0f, 0.5f);
    }
    
    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player1");
        if (player != null)
        {
            player.transform.position = checkpoint.transform.position;
        }
    }
    
}