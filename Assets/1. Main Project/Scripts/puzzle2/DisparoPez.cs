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
    public PlatformDestroyer platformDestroyer;
    
    
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && canShoot) // Solo el master client dispara los peces
        {
            float delay = Random.Range(minShootDelay, maxShootDelay);
            ShootFishesToEveryOne();
            canShoot = false;
        }
    }
    
    
    public void ShootFishesToEveryOne()
    {
        photonView.RPC("ShootFishRPC", RpcTarget.All);
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
        ShootFish();
        yield return new WaitForSeconds(2.4f);
        canShoot = true;
    }
    
    
    private void ShootFish()
    {
        LeanTween.moveLocalY(fishPrefab, -80f, 1.2f).setEaseInOutQuart().setOnComplete(ReturnToWater);
    }
    
    private void ReturnToWater()
    {
        LeanTween.rotateLocal(fishPrefab, new Vector3(-90f, 0, 0), 0.8f).setEaseInQuad();
        LeanTween.moveLocalY(fishPrefab, -93.5f, 1.2f).setEaseInOutQuart().setOnComplete(ResetXrotation);
        preshotParticleSystem.Stop();
    }
    public void ResetXrotation()
    {
        LeanTween.rotateLocal(fishPrefab, new Vector3(90f, 0, 0), 0.2f).setEaseInQuad();
    }
    
    IEnumerator DeactivatePlatform()
    {
        yield return new WaitForSeconds(0.85f);
        
    }
    
}