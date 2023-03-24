using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
   [SerializeField] private int targetFps = 60;
   
   void Awake()
   {
      Application.targetFrameRate = targetFps;
   }
}
