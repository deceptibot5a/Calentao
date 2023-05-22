using System;
using UnityEngine;

public class UI_Explosion : MonoBehaviour
{
    public ExplosionManager ExplosionManager;
    
    public ExplosionManager2 ExplosionManager2;

    
    public void AssingBomb()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ExplosionManager = GameObject.Find("Bomba01").GetComponent<ExplosionManager>();


    }
    
    public void AssingBomb2()
    {
        
        ExplosionManager2 = GameObject.Find("Bomba02").GetComponent<ExplosionManager2>();


    }
    
   public void BombExplosion1()
   
   {
       ExplosionManager.Explosion1();

   }
   
   public void BombExplosion2()
   
   {
       ExplosionManager2.Explosion2();

   }
   
   

}
