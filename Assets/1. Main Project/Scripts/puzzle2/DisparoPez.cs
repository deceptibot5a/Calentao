using System.Collections;
using Photon.Pun;
using UnityEngine;

public class DisparoPez : MonoBehaviour
{
    public GameObject fishPrefab;
    public ParticleSystem preshotParticleSystem;
    public float minShootDelay;
    public float maxShootDelay;
    public PhotonView photonView;
    private bool canShoot = true;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && canShoot)
        {
            photonView.RPC("ShootFishWithDelay", RpcTarget.All);
        }
    }

    [PunRPC]
    private IEnumerator ShootFishWithDelay()
    {
        canShoot = false;
        float delay = Random.Range(minShootDelay, maxShootDelay);
        yield return new WaitForSeconds(delay);

        StartCoroutine(ShootFishCoroutine(3f));
    }

    
    private IEnumerator ShootFishCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        preshotParticleSystem.Play();
        yield return new WaitForSeconds(3f);

        ShootFish(); 
        //LeanTween.rotateLocal(fishPrefab, new Vector3(-90f, 0, 0), 2.2f).setEaseInQuad(); .setEaseInOutQuart()

        canShoot = true;
    }

    private void ShootFish()
    {
        LeanTween.moveY(fishPrefab, 18f, 1.2f).setEaseInOutQuart().setOnComplete(ReturnToWater);
        
    }

    private void ReturnToWater()
    {
        LeanTween.rotateLocal(fishPrefab, new Vector3(-90f, 0, 0), 0.8f).setEaseInQuad();
        LeanTween.moveY(fishPrefab, -8f, 1.2f).setEaseInOutQuart().setOnComplete(RestartRotationX);
        preshotParticleSystem.Stop();
    }
    public void RestartRotationX()
    {
        LeanTween.rotateLocal(fishPrefab, new Vector3(90f, -89.603f, -1.024994f), 0.2f);
    }
    
}


