using UnityEngine;
using UnityEngine.Video;

public class CinematicEnded : MonoBehaviour
{
    [SerializeField] VideoPlayer cinematic;
    [SerializeField] private FadeToBlack fading;

    void Start() {
        cinematic.loopPointReached += EndCinematic;
    }

    private void EndCinematic(VideoPlayer vp) {
        fading.fadeIn = true;
        fading.firstTime = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
