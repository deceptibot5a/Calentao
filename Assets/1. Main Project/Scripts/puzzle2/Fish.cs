using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float gravity = 9.81f;
    public event Action<Fish> OnFishHitGround;
    

    private Rigidbody fishRigidbody;

    private void Start()
    {
        fishRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        fishRigidbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        transform.rotation = Quaternion.LookRotation(fishRigidbody.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grass"))
        {
            OnFishHitGround?.Invoke(this);
        }
        if (other.CompareTag("Platform"))
        {
           other.GetComponent<PlatformDestroyer>().DeactivatePlatform();
        }
    }
    
}

