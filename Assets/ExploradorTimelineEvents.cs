using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploradorTimelineEvents : MonoBehaviour
{
    
    private ExploradorPlayerController playerControllerex;
    private ExploradorCameraManager playerCamera;
    private ExploradorAnimatorManager playerAnimator;
    private SkinnedMeshRenderer playerSkinnedMesh;
    
    private void Start()
    {
        StartCoroutine(AssignPlayerInteractions());
    }
    
    IEnumerator AssignPlayerInteractions()
    {
        yield return new WaitForSeconds(0.1f);
        playerControllerex = GameObject.FindWithTag("Player1").GetComponent<ExploradorPlayerController>();
        playerCamera = GameObject.Find("CM vcam1 explorador").GetComponent<ExploradorCameraManager>();
        playerAnimator = GameObject.Find("ExploradorFINALv1").GetComponent<ExploradorAnimatorManager>();
        playerSkinnedMesh = GameObject.Find("ExploradorBody").GetComponent<SkinnedMeshRenderer>();

    }

    public void EnablePlayerMovement()

    {
        Debug.Log("Mover main character"); 
        playerControllerex.enabled = true;
        playerCamera.enabled = true;
        playerAnimator.enabled = true;
        playerSkinnedMesh.enabled = true; 
    }

    
}
