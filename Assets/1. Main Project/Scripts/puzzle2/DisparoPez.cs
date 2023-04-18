using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DisparoPez : MonoBehaviour
{
     public GameObject fishPrefab;
    public ParticleSystem preshotParticleSystem;
    public float minShootDelay = 1f;
    public float maxShootDelay = 3f;

    private bool canShoot = true;

    private void Update()
    {
        if (canShoot)
        {
            float delay = Random.Range(minShootDelay, maxShootDelay);
            StartCoroutine(ShootFish(delay));
            canShoot = false;
        }
    }

    private IEnumerator ShootFish(float delay)
    {
        yield return new WaitForSeconds(delay-2f);

        preshotParticleSystem.Play();
        
        
        yield return new WaitForSeconds(2f);

        GameObject fishObject = Instantiate(fishPrefab, transform.position, Quaternion.identity);
        fishObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 900f);

        Fish fish = fishObject.GetComponent<Fish>();
        fish.OnFishHitGround += OnFishHitGround;
    }

    private void OnFishHitGround(Fish fish)
    {
        preshotParticleSystem.Stop();
        fish.OnFishHitGround -= OnFishHitGround;
        Destroy(fish.gameObject);
        canShoot = true;
    }
}


