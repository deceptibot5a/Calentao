using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calentao.PlayerContol
{
public class EnableController : MonoBehaviour
{
    private bool assigned;
    private PlayerController playerController1;
    private PlayerController playerController2;
    public GameObject image; 
    
  
    void Update()
    {
        if (assigned == false)
        {
            playerController1 = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
            playerController2 = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
        if (playerController1 && playerController2  == null)
        {
            assigned = false;
        }
        else
        {
            assigned = true;
        } 
        
        }
        
    }
    public void EnableControllerPlayer()
    {
        playerController1.enabled = true;
        playerController2.enabled = true;
        image.SetActive(false);
    }
}
}
  

