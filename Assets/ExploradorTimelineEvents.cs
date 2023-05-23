using System.Collections;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class ExploradorTimelineEvents : MonoBehaviour
{
    
    private ExploradorPlayerController playerControllerex;
    private ExploradorCameraManager playerCamera;
    private ExploradorAnimatorManager playerAnimator;
    private SkinnedMeshRenderer playerSkinnedMesh;
    private CinemachineBrain _cinemachineBrain;
    private CanvasGroup exploradorCanvas; 
    
    PhotonView PV;
    
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
        _cinemachineBrain = GameObject.Find("ExpCinemachineBrain").GetComponent<CinemachineBrain>();
        exploradorCanvas = GameObject.Find("ExploradorCanvas").GetComponent<CanvasGroup>();  

    }

    [PunRPC]
    public void EnablePlayerMovement()

    {
        Debug.Log("Mover main character"); 
        PV.RPC("EnablePlayerMovement", RpcTarget.Others);
        _cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseOut;
        playerControllerex.enabled = true;
        playerCamera.enabled = true;
        playerAnimator.enabled = true;
        playerSkinnedMesh.enabled = true;
        exploradorCanvas.DOFade(1, 0.5f);
    }

    
}
