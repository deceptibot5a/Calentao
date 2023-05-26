using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFX : MonoBehaviour
{

    public AudioSource uiSource;
    public AudioClip clickButtonSFX;
    public AudioClip inputButtonSFX; 
    public AudioClip backButtonSFX; 
    
    public void clickButton()
    {
        uiSource.PlayOneShot(clickButtonSFX);
    }
    
    public void InputButton()
    {
        uiSource.PlayOneShot(inputButtonSFX);
    }
    
    public void backButton()
    {
        uiSource.PlayOneShot(backButtonSFX);
    }
    
    

}
