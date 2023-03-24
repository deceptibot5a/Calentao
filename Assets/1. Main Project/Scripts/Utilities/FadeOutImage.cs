using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutImage : MonoBehaviour
{
    public float fadeDuration = 7f; // Duración de la transición de fade out
    public float startDelay = 0f; // Retardo inicial antes de comenzar la transición

    private Image image;

    private void Start()
    {
        // Obtener la referencia a la imagen
        image = GetComponent<Image>();

        // Iniciar la transición de fade out
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        // Retardo inicial
        yield return new WaitForSeconds(startDelay);

        // Calcular el tiempo que tomará la transición
        float timeLeft = fadeDuration;
        float alpha;

        // Bucle mientras la imagen esté visible
        while (timeLeft > 0f)
        {
            // Calcular el alpha actual
            alpha = timeLeft / fadeDuration;

            // Establecer el alpha de la imagen
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Esperar un frame
            yield return null;

            // Actualizar el tiempo restante
            timeLeft -= Time.deltaTime;
        }

        // Asegurarse de que el alpha sea 0
        Color finalColor = image.color;
        finalColor.a = 0f;
        image.color = finalColor;

        // Destruir el objeto de la imagen
        Destroy(gameObject);
    }
}