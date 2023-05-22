using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Explosion : MonoBehaviour
{
    public ExplosionManager ExplosionManager; 
    

    public void AssingBomb()
    {
        
        ExplosionManager = GameObject.Find("Bomba01").GetComponent<ExplosionManager>();


    }
    
   public void BombExplosion1()
   
   {
       ExplosionManager.Explosion1();

   }

}
