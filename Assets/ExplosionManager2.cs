using UnityEngine;
using UnityEngine.UI; 



public class ExplosionManager2 : MonoBehaviour
{
    public UI_Explosion UIExplosion;
    
    
    public BombManager01 bombManager01;
    
    public GameObject button2; 
    
    private void Start()
    {
        
        bombManager01 = GameObject.Find("ExplosionManager01").GetComponent<BombManager01>();
        
        UIExplosion = GameObject.Find("CanvasCameras").GetComponent<UI_Explosion>();
        
        
        button2 = bombManager01.gameObjectsList.Find(obj => obj.name == "BombButton2");

        button2.SetActive(true);
        
        UIExplosion.AssingBomb2();
        

    }
    
    public void Explosion2()
    {
        button2.SetActive(false);
        Debug.Log("Cinematica FINAl");
    }
    

}
