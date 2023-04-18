using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float distance = 3f;
    private Transform raycastTransform;
    private Transform highlight;
    private bool highlighted = false;
    private Puzzle2 puzzle;
    

    public bool canray = false;


    private void Awake()
    {
        puzzle = FindObjectOfType<Puzzle2>();
    }

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
                        Puzzle2.highlighted = true;

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
                    Puzzle2.highlighted = false;
                }
            }   
        }
    }
    
    public void clicked(InputAction.CallbackContext obj)
    {
        if (highlight == null) return; 
        
        if (highlight.CompareTag("Selectable"))
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);
                hit.collider.gameObject.GetComponent<Puzzle2Button>().buttonclick();
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

