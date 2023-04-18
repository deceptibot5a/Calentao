using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
   public int targetFPS = 60;

   void Start()
   {
       //QualitySettings.vSyncCount = 0;
       Application.targetFrameRate = targetFPS;
   }

}
