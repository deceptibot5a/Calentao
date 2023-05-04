using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialChangers : MonoBehaviour
{
   public Material nuevoMaterial; 
   public Renderer renderer;
   public PhotonView photonView;

   private void Start()
   {
        StartCoroutine(AssignPhotonView());
   }

   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.CompareTag("Player1") && photonView.IsMine)
       {
              renderer.material = nuevoMaterial;
       }
   }
   IEnumerator AssignPhotonView()
   {
       yield return new WaitForSeconds(0.1f);
       photonView = GameObject.FindWithTag("Player1").GetComponent<PhotonView>();
   }
       
}
