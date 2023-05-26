using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public bool isIntroEnded = false;
    public static VideoPlayerController instance;
    
    private void Awake()
    {
        instance = this;
    }
    
    
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.UnloadScene("IntroScene");
        isIntroEnded = true;
        MenuManager_Test.Instance.timeLine.SetActive(true);
    }
}