using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersAudioManager : MonoBehaviour
{
 
    [Header("Player Audios")]
    public AudioSource _walkAudioSource;
    public AudioSource _runAudioSource;
    public  AudioSource _breathingNormalAudioSource;
    public  AudioSource _breathingRunAudioSource;
    
    
    public void FadeAudios()
    {
        StartCoroutine(FadePlayerAudios());
    }
        
        

     private IEnumerator FadePlayerAudios()
        {
            yield return new WaitForSeconds(0.1f);
           
            Debug.Log("AudiosFades");
            if (!_breathingNormalAudioSource.isPlaying)
            {
                _breathingNormalAudioSource.Play();
            }
            float fadeTime = 0.1f; // tiempo para desvanecer el sonido

            // Desvanece el volumen del _breathingNormalAudioSource
            float startVolume = _breathingNormalAudioSource.volume;
            float endVolume = 0.2f;
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _breathingNormalAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _breathingNormalAudioSource.volume = endVolume;

            // Desvanece el volumen del _breathingRunAudioSource
            startVolume = _breathingRunAudioSource.volume;
            endVolume = 0f;
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _breathingRunAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _breathingRunAudioSource.volume = endVolume;

            // Desvanece el volumen del _walkAudioSource
            startVolume = _walkAudioSource.volume;
            endVolume = 0f;
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _walkAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _walkAudioSource.volume = endVolume;

            // Desvanece el volumen del _runAudioSource
            startVolume = _runAudioSource.volume;
            endVolume = 0f;
            t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _runAudioSource.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }
            _runAudioSource.volume = endVolume;
            
            yield return new WaitForSeconds(1f);
           
           
            
        }
    // Update is called once per frame
    void Update()
    {
        
    }
}
