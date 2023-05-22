
using UnityEngine;
using Cinemachine;
public class CameraPlayerAssing2 : MonoBehaviour
{
    public static CameraPlayerAssing2 instance;

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