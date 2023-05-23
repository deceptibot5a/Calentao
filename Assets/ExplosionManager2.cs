using UnityEngine;
using Photon.Pun;


public class ExplosionManager2 : MonoBehaviour
{
    public UI_Explosion UIExplosion;
    
    
    public BombManager01 bombManager01;
    
    public GameObject button2; 
    
    public PhotonView PV;
    
    
    
    private void Start()
    {
        
        bombManager01 = GameObject.Find("ExplosionManager01").GetComponent<BombManager01>();
        
        UIExplosion = GameObject.Find("CanvasCameras").GetComponent<UI_Explosion>();
        
        
        button2 = bombManager01.gameObjectsList.Find(obj => obj.name == "BombButton2");

        button2.SetActive(true);
        
        UIExplosion.AssingBomb2();
        

    }
    
 
    
    [PunRPC]
    public void ExplosionRPC2()
    
    {
        button2.SetActive(false);
        Checkpoints.instance.TurnOnDeathPanel();
        Debug.Log("Cinematica FINAl");
    }
    
    public void Explosion2()
    {
        PV.RPC("ExplosionRPC2", RpcTarget.All); 
    }
    

}
