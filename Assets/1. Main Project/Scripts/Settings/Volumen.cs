using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Volumen : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = AudioListener.volume;

        slider.onValueChanged.AddListener(ActualizarVolumen);
    }

    public void ActualizarVolumen(float nuevoVolumen)
    {
        AudioListener.volume = nuevoVolumen;
    }
}
