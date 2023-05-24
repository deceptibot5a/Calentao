using System.Collections;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;


public class ExplosionManager : MonoBehaviour
{
    public GameObject originalWall;
    public GameObject fracturedWall;
    public GameObject explosionVFX;

    public UI_Explosion UIExplosion;

    public GameObject button1; 

    public BombManager01 bombManager01; 

    public MeshRenderer bombMeshRenderer;

    public AudioSource explosionSource;

    public AudioClip explosionSOUND; 
    

    public Material targetMaterial; 
    //public GameObject rock;
    public bool CantExplode = true;
    public float fragScaleFactor = 1;

    public PhotonView PV; 

    private void Start()
    {
        UIExplosion = GameObject.Find("CanvasCameras").GetComponent<UI_Explosion>();
        
        
        UIExplosion.AssingBomb();
        
        bombManager01 = GameObject.Find("ExplosionManager01").GetComponent<BombManager01>();
        
        originalWall = bombManager01.gameObjectsList.Find(obj => obj.name == "Wall_FULL");
        
        fracturedWall = bombManager01.gameObjectsList.Find(obj => obj.name == "Wall_FRACTURE");
        
        explosionVFX = bombManager01.gameObjectsList.Find(obj => obj.name == "ExplosionVFX");

        button1 = bombManager01.gameObjectsList.Find(obj => obj.name == "BombButton1"); 
        
        explosionSource = bombManager01.gameObjectsList.Find(obj => obj.name == "SFX_EXPLORADOR").GetComponent<AudioSource>();
        
        button1.SetActive(true);

    }
    
    IEnumerator Shrink ()
    {
        targetMaterial.DOFade(1f, 0f); 
        yield return new WaitForSeconds(2f);
        targetMaterial.DOFade(0f, 2f);
        yield return new WaitForSeconds(1.8f);
        fracturedWall.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        
       
        Debug.Log("Disabled");

        
    }
    
    [PunRPC]
    public void ExplosionRPC()
    
    {
        if (!CantExplode)
        {
            explosionSource.PlayOneShot(explosionSOUND);
            originalWall.SetActive(false);
            fracturedWall.SetActive(true);
            explosionVFX.SetActive(true);
            bombMeshRenderer.enabled = false; 
            //rock.transform.parent = null; 
            CantExplode = true;
            button1.SetActive(false);
            StartCoroutine(Shrink());
        }
        
    }
    
    public void Explosion1()

    {
        PV.RPC("ExplosionRPC", RpcTarget.All); 
    }
    
}
