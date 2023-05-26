using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Calidad : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private int calidad;
    void Start()
    {
        calidad = PlayerPrefs.GetInt("numeroCalidad", 2);
        dropdown.value = calidad;
        AjustarCalidad();
    }
    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("numeroCalidad", dropdown.value);
        calidad = dropdown.value;
    }
}
