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
        yield return new WaitForSeconds(2f);

        ShootFish();
        

        canShoot = true;
    }

    private void ShootFish()
    {
        LeanTween.moveLocalY(fishPrefab, 6.5f, 1.2f).setEaseInOutQuart().setOnComplete(ReturnToWater);
        
    }

    private void ReturnToWater()
    {
        LeanTween.moveLocalY(fishPrefab, -8f, 1.8f).setEaseOutSine().setOnComplete(RestartRotationX);
        LeanTween.rotateLocal(fishPrefab, new Vector3(-90f, 0, 0), 0.6f).setEaseInQuad();
        preshotParticleSystem.Stop();
    }
    public void RestartRotationX()
    {
        LeanTween.rotateLocal(fishPrefab, new Vector3(90f, 0, 0), 0.2f);
    }
    
}


