using UnityEngine;
using Photon.Pun;


public class ExplosionManager2 : MonoBehaviour
{
    public UI_Explosion UIExplosion;
    
    
    public BombManager01 bombManager01;
    
    public GameObject button2; 
    
    public PhotonView PV;

    WinManager winManag;

    public GameObject allScene;
    
    private void Start()
    {
        
        bombManager01 = GameObject.Find("ExplosionManager01").GetComponent<BombManager01>();
        
        UIExplosion = GameObject.Find("CanvasCameras").GetComponent<UI_Explosion>();
        
        
        button2 = bombManager01.gameObjectsList.Find(obj => obj.name == "BombButton2");
        
        allScene = bombManager01.gameObjectsList.Find(obj => obj.name == "AllScene");

        button2.SetActive(true);
        
        UIExplosion.AssingBomb2();

        winManag = FindObjectOfType<WinManager>();
    }
    
 
    
    [PunRPC]
    public void ExplosionRPC2()
    
    {
        button2.SetActive(false);
        //Checkpoints.instance.TurnOnDeathPanel();
        WinManager.instance.WinTheGame();
        Debug.Log("Cinematica FINAl");
        Timer.instance.StopTimer();
        allScene.SetActive(false);
    }
    
    public void Explosion2()
    {
        PV.RPC("ExplosionRPC2", RpcTarget.All); 
    }
    

}
