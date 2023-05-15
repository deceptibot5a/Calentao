using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 


public class ExplosionManager : MonoBehaviour
{
    public GameObject originalWall;
    public GameObject fracturedWall;
    public GameObject explosionVFX; 
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
            //rock.transform.parent = null; 
            CantExplode = true;

          StartCoroutine(Shrink()); 
        }
    }
    IEnumerator Shrink ()
    {
        yield return new WaitForSeconds(3f);
        fracturedWall.transform.DOScale (new Vector3 (0f, 0f, 0f), 3f);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Disabled");

        
    }
}
