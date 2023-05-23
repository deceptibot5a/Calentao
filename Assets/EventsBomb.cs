using System.Collections;
using Photon.Pun;
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
    public GameObject areaPlant02;

    public ExplosionManager explosionManager; 
    public ExplosionManager2 explosionManager02; 

    public PlantBombManager PlantBombManager; 

    public GameObject cameraMain;
    public GameObject bombCamera; 
    
    public Transform ExploradorOriginalPosition;
    public GameObject exploradorCurrentPosition;
    public GameObject Bomb01GameObject;
    public GameObject Bomb02GameObject;

    public PhotonView PV;
    
    private void Start()
    {
     
        StartCoroutine(AssignAreas());
       
    }
    
    IEnumerator AssignAreas()
    {
        yield return new WaitForSeconds(0.5f);
  
        areaPlant01 = GameObject.Find("AreaPlant01");
        areaPlant02 = GameObject.Find("AreaPlant02");

    }
 
    public void PlantFinish()
    {
        Debug.Log("Plantado exitoso");
        plantBombManager.planting = false;
        plantBombManager.canPlant = false;

        
        if (PV.IsMine)
        {
        cameraMain.SetActive(true);
        }
        bombCamera.SetActive(false);
        exploradorAnimatorManager.enabled = true;
        exploradorPlayerController.enabled = true;
        ExploradorCameraManager.enabled = true;
        characterController.enabled = true;
        animator.SetBool("Planting", false);
        
        
        if (plantBombManager.bomb1)
        { 
            explosionManager.enabled = true;
        Bomb01GameObject.transform.parent = null; 
        areaPlant01.SetActive(false);
        }
        
        if (plantBombManager.bomb2)
        {
            explosionManager02.enabled = true;
            areaPlant02.SetActive(false);
            Bomb02GameObject.transform.parent = null; 
        }
        
        StartCoroutine(ExploradorOriginalPositionCorrutine()); 
       
    }
    
    public void SpawnBomb()
    
    {
        if (plantBombManager.bomb1)
        {
        Bomb01GameObject.SetActive(true);
        }
        
        if (plantBombManager.bomb2)
        {
            Bomb02GameObject.SetActive(true);
        }
        
        
       
    }
    
    
    private IEnumerator ExploradorOriginalPositionCorrutine()
    {

        yield return new WaitForSeconds(0.5f); 
        Debug.Log("Position Original");
        exploradorCurrentPosition.transform.position = ExploradorOriginalPosition.transform.position; 
        exploradorCurrentPosition.transform.rotation = ExploradorOriginalPosition.transform.rotation;

    }
 
}