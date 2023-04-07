using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float distance = 3f;
    private Transform raycastTransform;
    private Transform highlight;

    public bool canray = false;
    
    private void Update()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        
        if (canray)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = distance;
            Vector3 worldPosition = camera.ScreenToWorldPoint(mousePosition);
    
            Ray ray = new Ray(camera.transform.position, worldPosition - camera.transform.position);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
    
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, distance))
            {
                highlight = hitinfo.transform;
                if (highlight.CompareTag("Selectable"))
                {
                    if (highlight.gameObject.GetComponent<Outline>() != null)
                    {
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = highlight.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                        highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                    }
                }
                else
                {
                    highlight = null;
                }
            }   
        }
    }

    [PunRPC]
    void SyncInteractedObject(int viewID)
    {
        PhotonView photonView = PhotonView.Find(viewID);
        if (photonView != null)
        {
            ButtonManager buttonManager = photonView.GetComponent<ButtonManager>();
            if (buttonManager != null)
            {
                buttonManager.isInteracted = true;
                buttonManager.Interacted();
            }
        }
    }
}

