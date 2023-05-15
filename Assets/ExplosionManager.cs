using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 


public class ExplosionManager : MonoBehaviour
{
    public GameObject originalWall;
    public GameObject fracturedWall;
    public GameObject explosionVFX;

    public MeshRenderer bombMeshRenderer; 
    

    public Material targetMaterial; 
   //public GameObject rock;
    public bool CantExplode = true;
    public float fragScaleFactor = 1; 


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
        yield return new WaitForSeconds(3f);
        targetMaterial.DOFade(0f, 2f); 
        //fracturedWall.transform.DOScale (new Vector3 (0f, 0f, 0f), 3f);
        yield return new WaitForSeconds(2f);
        fracturedWall.SetActive(false);
        
       
        Debug.Log("Disabled");

        
    }
}
