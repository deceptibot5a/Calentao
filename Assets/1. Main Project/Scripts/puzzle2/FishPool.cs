using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{
    public GameObject fishPrefab;
    public int poolSize = 10;

    private List<GameObject> fishPool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject fishObject = Instantiate(fishPrefab, transform.position, Quaternion.identity);
            fishObject.SetActive(false);
            fishPool.Add(fishObject);
        }
    }

    public GameObject GetFish()
    {
        foreach (GameObject fishObject in fishPool)
        {
            if (!fishObject.activeInHierarchy)
            {
                fishObject.SetActive(true);
                return fishObject;
            }
        }

        return null;
    }

    public void ReturnFish(GameObject fishObject)
    {
        fishObject.SetActive(false);
    }
}