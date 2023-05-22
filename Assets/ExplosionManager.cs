using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
using UnityEngine.UI; 


public class ExplosionManager : MonoBehaviour
{
    public GameObject originalWall;
    public GameObject fracturedWall;
    public GameObject explosionVFX;

    public UI_Explosion UIExplosion;

    public Button bombButton; 

    public BombManager01 bombManager01; 

    public MeshRenderer bombMeshRenderer; 
    

    public Material targetMaterial; 
    //public GameObject rock;
    public bool CantExplode = true;
    public float fragScaleFactor = 1;

    private void Start()
    {
        UIExplosion = GameObject.Find("CanvasCameras").GetComponent<UI_Explosion>();
        
        bombButton = GameObject.Find("BombButton1").GetComponent<Button>();

        bombButton.interactable = true;  
        
        UIExplosion.AssingBomb();
        
        bombManager01 = GameObject.Find("ExplosionManager01").GetComponent<BombManager01>();
        
        originalWall = bombManager01.gameObjectsList.Find(obj => obj.name == "Wall_FULL");
        
        fracturedWall = bombManager01.gameObjectsList.Find(obj => obj.name == "Wall_FRACTURE");
        
        explosionVFX = bombManager01.gameObjectsList.Find(obj => obj.name == "ExplosionVFX");


    }
    
    IEnumerator Shrink ()
    {
        targetMaterial.DOFade(1f, 0f); 
        yield return new WaitForSeconds(2f);
        targetMaterial.DOFade(0f, 2f);
        yield return new WaitForSeconds(1.8f);
        fracturedWall.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        
       
        Debug.Log("Disabled");

        
    }
    
    public void Explosion1()
    
    {
        if (!CantExplode)
        {
            originalWall.SetActive(false);
            fracturedWall.SetActive(true);
            explosionVFX.SetActive(true);
            bombMeshRenderer.enabled = false; 
            //rock.transform.parent = null; 
            CantExplode = true;

            StartCoroutine(Shrink());
        }
        
    }
    
}
