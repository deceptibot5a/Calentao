using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager01 : MonoBehaviour
{
    public List<GameObject> gameObjectsList;
    
    public void AddGameObject(GameObject obj)
    {
        gameObjectsList.Add(obj);
    }
}
