using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DisparoPez : MonoBehaviour
{
     public GameObject fishPrefab;
    public ParticleSystem preshotParticleSystem;
    public float minShootDelay = 1f;
    public float maxShootDelay = 3f;
    public PhotonView photonView;
    private bool canShoot = true;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && canShoot) // Solo el master client dispara los peces
        {
            float delay = Random.Range(minShootDelay, maxShootDelay);
            photonView.RPC("ShootFishRPC", RpcTarget.All, delay);
            canShoot = false;
        }
    }
    [PunRPC]
    private void ShootFishRPC(float delay)
    {
        StartCoroutine(ShootFish(delay));
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


