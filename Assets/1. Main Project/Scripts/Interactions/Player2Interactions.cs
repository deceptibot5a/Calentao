


using UnityEngine;
using Photon.Pun;

public class Player2Interactions : MonoBehaviour
{
    [SerializeField] private Camera camera; 
    [SerializeField] private float distance = 3f;
    [SerializeField] private Color highlightColor = Color.magenta;
    [SerializeField] private float highlightWidth = 7f;
    
    public GameObject virtualCam;
    
    public bool canray = false;
    private Transform highlightedObject;
    private Outline highlightedOutline;
    private Puzzle2Button highlightedButton;
    
    PhotonView PV;

    
    private void Start()
    {
        if (!PV.IsMine)
        {
            virtualCam.SetActive(false);

        }
    }

    private void FixedUpdate()
    {
        if(!PV.IsMine)
            return;
  
    }

    

    private void Awake()
    {
        highlightedButton = FindObjectOfType<Puzzle2Button>();
        PV = GetComponent<PhotonView>();
    }
    
    private void Update()
    {
        if(!PV.IsMine)
            return;
        if (!canray) return;
        // Unhighlight the previously highlighted object
        if (highlightedObject != null)
        {
            highlightedOutline.enabled = false;
            highlightedObject = null;
            highlightedOutline = null;
            highlightedButton = null;
        }
        
        // Perform a raycast to detect if the mouse is over an object
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            // If the object is selectable, highlight it
            if (hit.collider.CompareTag("Selectable"))
        
            {
                highlightedObject = hit.transform;
                highlightedOutline = highlightedObject.gameObject.GetComponent<Outline>();
                if (highlightedOutline == null)
                {
                    highlightedOutline = highlightedObject.gameObject.AddComponent<Outline>();
                    highlightedOutline.OutlineColor = highlightColor;
                    highlightedOutline.OutlineWidth = highlightWidth;
                }
                highlightedOutline.enabled = true;

                // Check if the highlighted object has the Puzzle2Button component
                if (highlightedObject.GetComponent<Puzzle2Button>() != null)
                {
                    highlightedButton = highlightedObject.GetComponent<Puzzle2Button>();
                    highlightedButton.Highlighted = true;
                }
            }
            
        }
        
    }


    private void OnMouseExit()

    {
        // If the mouse exits the object, remove the highlight
        if (highlightedObject != null)
        {
            highlightedOutline.enabled = false;
            highlightedObject = null;
            highlightedOutline = null;

            // Reset the Highlighted bool on the previously highlighted Puzzle2Button component
            if (highlightedButton != null)
            {
                highlightedButton.Highlighted = false;
                highlightedButton = null;
            }
        }
    }
}

