using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private CanvasGroup black;
    [SerializeField] private CanvasGroup cinematic;
    public bool fadeIn = false;
    private bool fadeOut = false;
    public bool firstTime = true;

    void Start() {
        fadeIn = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (fadeIn) {
            if (black.alpha < 1f) {
                black.alpha += Time.deltaTime;
                if (black.alpha >= 1f) {
                    fadeIn = false;
                    Invoke("ActivateCinematic", 1f);
                }
            }
        }
        if (fadeOut && firstTime) {
            if (black.alpha >= 0) {
                black.alpha -= Time.deltaTime;
                if (black.alpha == 0) {
                    fadeOut = false;
                }
            }
        }
    }

    private void ActivateCinematic() {
        cinematic.alpha = 1;
        fadeOut = true;
    }
}
