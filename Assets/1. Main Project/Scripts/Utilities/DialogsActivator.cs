using System.Collections;
using UnityEngine;

public class DialogsActivator : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject audioBox;
    public float dialogTime = 10f;
    private bool wasActivated = false;

    public AudioSource audioGarbanzo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            if (!wasActivated) // Verificar si no ha sido activado previamente
            {
                wasActivated = true; // Marcar como activado
                if (dialogBox != null)
                {
                    StartCoroutine(MoveDialog());
                    Debug.Log("Dialog Activated");
                }

                if (audioGarbanzo != null)
                {
                    ActiveAudio();
                    StartCoroutine(moveAudioDialog());
                }
            }
        }
    }

    public void ActiveAudio()
    {
        audioGarbanzo.Play();
    }

    IEnumerator MoveDialog()
    {
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveX(dialogBox, 115, 0.5f).setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(dialogTime);
        LeanTween.moveX(dialogBox, -700, 0.5f).setEase(LeanTweenType.easeInOutBack).setOnComplete(TurnOffDialog);
    }

    IEnumerator moveAudioDialog()
    {
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveX(audioBox, 60, 0.5f).setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(dialogTime);
        LeanTween.moveX(audioBox, -700, 0.5f).setEase(LeanTweenType.easeInOutBack);
    }

    private void TurnOffDialog()
    {
        dialogBox.SetActive(false);
    }
}