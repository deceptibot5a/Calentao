using System.Collections;
using System.Collections.Generic;
using AlekGames.HoverCraftSystem.Systems.Addons;
using AlekGames.HoverCraftSystem.Systems.Main;
using UnityEngine;

public class ExploradorBikeAnimationsManager : MonoBehaviour
{
    public Transform LeftMediumTransform;
    public Transform LeftFinalTransform;
    public GameObject playerMesh;
    public hoverCraft hoverCraft;
    public hoverCraftTilt hoverCraftTilt; 

    public void BikeLeftFinalTransform()
    {
        StartCoroutine(DoBikeLeftFinalTransform());
    }

    private IEnumerator DoBikeLeftFinalTransform()
    {
        yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos antes de hacer la transformación
        hoverCraft.enabled = true;
        hoverCraftTilt.enabled = true; 
        playerMesh.transform.position = LeftFinalTransform.position; 
        playerMesh.transform.rotation = LeftFinalTransform.rotation;
    }
    
    public void BikeLeftMediumTransform()
    {
        StartCoroutine(DoBikeLeftMediumTransform());
    }
    
    private IEnumerator DoBikeLeftMediumTransform()
    {
        yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos antes de hacer la transformación
        playerMesh.transform.position = LeftMediumTransform.position; 
        playerMesh.transform.rotation = LeftMediumTransform.rotation;
    }
    
   
}
