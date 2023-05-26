using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class DialogsActivator2 : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject audioBox;
    public float dialogTime = 10f;
    public bool wasActivated = false;
    
    
    public AudioSource audioGarbanzo;
    public PlayableDirector reto3; 
    
    
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
    
    public void ActiveAudio ()
    {
        reto3.Play();
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
