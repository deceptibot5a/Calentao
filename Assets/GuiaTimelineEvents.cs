using System.Collections;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using Photon.Pun;

public class GuiaTimelineEvents : MonoBehaviour
{
    private GuiaPlayerController playerControllerex;
    private GuiaCameraManager playerCamera;
    private GuiaAnimationManager playerAnimator;
    private SkinnedMeshRenderer playerSkinnedMesh;
    private CinemachineBrain _cinemachineBrain;
    private CanvasGroup guiaCanvas; 
    
    PhotonView PV;

    private void Start()
    {
        StartCoroutine(AssignPlayerInteractions());
    }
    
    IEnumerator AssignPlayerInteractions()
    {
        yield return new WaitForSeconds(0.1f);
        playerControllerex = GameObject.FindWithTag("Player2").GetComponent<GuiaPlayerController>();
        playerCamera = GameObject.Find("CM vcam1 Guia").GetComponent<GuiaCameraManager>();
        playerAnimator = GameObject.Find("GuiaMesh").GetComponent<GuiaAnimationManager>();
        playerSkinnedMesh = GameObject.Find("GuiaBody").GetComponent<SkinnedMeshRenderer>();
        _cinemachineBrain = GameObject.Find("GuiaCinemachineBrain").GetComponent<CinemachineBrain>();
        guiaCanvas = GameObject.Find("GuiaCanvas").GetComponent<CanvasGroup>();  

    }
    
    [PunRPC]
    public void EnableGuiaMovement()

    {
        Debug.Log("Guia Enabled"); 
        PV.RPC("EnableGuiaMovement", RpcTarget.Others);
        _cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseOut;
        playerControllerex.enabled = true;
        playerCamera.enabled = true;
        playerAnimator.enabled = true;
        playerSkinnedMesh.enabled = true;
        guiaCanvas.DOFade(1, 0.5f);
    }
}
