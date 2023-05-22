using Cinemachine;
using UnityEngine;

public class CameraPlayerAssing : MonoBehaviour
{
    public static CameraPlayerAssing instance;

    [SerializeField] private CinemachineVirtualCamera cam; 

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this; 
    }
    
    public void EnableCamera()
    {
        cam.enabled = true; 
    }

}
