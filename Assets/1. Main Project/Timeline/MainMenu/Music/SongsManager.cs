using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongsManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> queue;
    private bool isPlayingDefault = true;

    void Start()
    {
     
        audioSource.Play();
    }

    void Update()
    {
    
        if (isPlayingDefault && !audioSource.isPlaying)
        {
            isPlayingDefault = false;
            PlayQueue();
        }
    }

    void PlayQueue()
    {
        // Reproduce las canciones de la cola en bucle
        StartCoroutine(PlayQueueCoroutine());
    }

    IEnumerator PlayQueueCoroutine()
    {
        while (true)
        {
            foreach (AudioClip clip in queue)
            {
                // Asigna la siguiente canción de la cola al AudioSource y espera a que termine
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);
            }
        }
    }
}