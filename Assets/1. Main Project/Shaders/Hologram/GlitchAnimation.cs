using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class GlitchAnimation : MonoBehaviour
{
    [Header("Glitch")] 
    [SerializeField] private Vector2 timeRange;

    [SerializeField] private float timeWait = 0.2f;

    private Material _material;
    private int _hash_useGlitch = Shader.PropertyToID("_Use_Glitch");

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        StartCoroutine(StartGlitch());
    }

    private IEnumerator StartGlitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeRange.x, timeRange.y));
            
            _material.SetFloat(_hash_useGlitch, 1 );

            yield return new WaitForSeconds(timeWait);
            
            _material.SetFloat(_hash_useGlitch, 0);
        }
    }
}