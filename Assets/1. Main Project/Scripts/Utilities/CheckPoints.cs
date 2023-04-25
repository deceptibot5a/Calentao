using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckPoints : MonoBehaviour
{
    public Transform checkpoint;
    public CanvasGroup deathPanel;
    public float fadeTime = 0.3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            StartCoroutine(DeathPanelFade(deathPanel, fadeTime));
            other.transform.position = checkpoint.position;
        }
    }

    IEnumerator DeathPanelFade(CanvasGroup deathPanel, float fadeTime)
    {
        // Aumentar gradualmente el alpha del panel de muerte
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            deathPanel.alpha = Mathf.Clamp01(t);
            yield return null;
        }

        // Establecer manualmente el valor alpha a 1 para asegurarse de que alcance el valor exacto
        deathPanel.alpha = 1.0f;

        // Esperar un momento
        yield return new WaitForSeconds(2f);

        // Disminuir gradualmente el alpha del panel de muerte
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            deathPanel.alpha = Mathf.Clamp01(1 - t);
            yield return null;
        }

        // Asegurarse de que el valor alpha sea 0 al finalizar la interpolación
        deathPanel.alpha = 0.0f;
    }
}