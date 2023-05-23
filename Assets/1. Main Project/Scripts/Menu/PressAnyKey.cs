using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class PressAnyKey : MonoBehaviour
{
    public GameObject welcomePanel;
    public GameObject background;
    public PlayableDirector musicDirector;
    public UI_SFX SFX; 
    private void FixedUpdate()
    {
        if (Input.anyKey && MenuManager_Test.Instance.isLoading == false)
        {
            SFX.clickButton();
            musicDirector.Play();
            background.GetComponent<Image>().raycastTarget = false;
            welcomePanel.GetComponent<PressAnyKey>().enabled = false;
            LeanTween.alpha(welcomePanel.GetComponent<RectTransform>(), 0f, 0.3f).setOnComplete(DeactivateWelcomePanel); 
            LeanTween.alphaCanvas(MenuManager_Test.Instance.loadingPlanetImage.GetComponent<CanvasGroup>(), 0f, 0.3f);
          
    
           
        }
    }
    
    public void DeactivateWelcomePanel()
    {
        MenuManager_Test.Instance.ActivateBlackBackground();
        MenuManager_Test.Instance.OpenContract();
        welcomePanel.SetActive(false);
    }
}
