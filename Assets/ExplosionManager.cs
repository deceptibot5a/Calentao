using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 


public class ExplosionManager : MonoBehaviour
{
    public GameObject originalWall;
    public GameObject fracturedWall;
    public GameObject explosionVFX;

    public BombManager01 bombManager01; 

    public MeshRenderer bombMeshRenderer; 
    

    public Material targetMaterial; 
   //public GameObject rock;
    public bool CantExplode = true;
    public float fragScaleFactor = 1;

    private void Start()
    {
        bombManager01 = GameObject.Find("ExplosionManager01").GetComponent<BombManager01>();
        
        originalWall = bombManager01.gameObjectsList.Find(obj => obj.name == "Wall_FULL");
        
        fracturedWall = bombManager01.gameObjectsList.Find(obj => obj.name == "Wall_FRACTURE");
        
        explosionVFX = bombManager01.gameObjectsList.Find(obj => obj.name == "ExplosionVFX");


    }
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !CantExplode)
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
}
