using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   public static SpawnManager Instance;
   
   
   void Awake()
   {
      Instance = this;
   }
}
