using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RealTimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI textHora;
    private float ultimaActualizacion = 0f;
    private const float intervaloActualizacion = 1f;

    void Start()
    {
        textHora = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Time.time - ultimaActualizacion > intervaloActualizacion)
        {
            ultimaActualizacion = Time.time;
            textHora.text = System.DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
