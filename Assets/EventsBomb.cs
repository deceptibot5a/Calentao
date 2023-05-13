using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsBomb : MonoBehaviour
{
    public Animator animator;
    public ExploradorPlayerController exploradorPlayerController;
    public ExploradorAnimatorManager exploradorAnimatorManager;
    public ExploradorCameraManager ExploradorCameraManager;
    public CharacterController characterController;
    public PlantBombManager plantBombManager;
    public GameObject areaPlant01; 

    public GameObject cameraMain;
    public GameObject bombCamera; 
    
    public Transform ExploradorOriginalPosition;
    public GameObject exploradorCurrentPosition;
    public GameObject Bomb01GameObject; 
 
    public void PlantFinish()
    {
        Debug.Log("Plantado exitoso");
        plantBombManager.planting = false;
        plantBombManager.canPlant = false; 
        areaPlant01.SetActive(false);
  
        cameraMain.SetActive(true);
        bombCamera.SetActive(false);
        exploradorAnimatorManager.enabled = true;
        exploradorPlayerController.enabled = true;
        ExploradorCameraManager.enabled = true;
        characterController.enabled = true;
        animator.SetBool("Planting", false);
        
        Bomb01GameObject.transform.parent = null; 
        
        StartCoroutine(ExploradorOriginalPositionCorrutine()); 
       
    }
    
    public void SpawnBomb()
    
    {
        Bomb01GameObject.SetActive(true);
       
    }
    
    public void BombMeshParent()

    {
       

    }
    
    private IEnumerator ExploradorOriginalPositionCorrutine()
    {

        yield return new WaitForSeconds(0.5f); 
        Debug.Log("Position Original");
        exploradorCurrentPosition.transform.position = ExploradorOriginalPosition.transform.position; 
        exploradorCurrentPosition.transform.rotation = ExploradorOriginalPosition.transform.rotation;

    }
 
}