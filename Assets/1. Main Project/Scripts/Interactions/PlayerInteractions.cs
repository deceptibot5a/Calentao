using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float distance = 3f;
    private Transform raycastTransform;
    
    private void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);

        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, distance))
        {
            raycastTransform = hitinfo.transform;

            if (raycastTransform.CompareTag("Selectable") && Mouse.current.leftButton.wasPressedThisFrame)
            {
                var buttonManager = raycastTransform.gameObject.GetComponent<ButtonManager>();

                if (buttonManager != null && !buttonManager.photonView.IsMine)
                {
                    return;
                }

                if (buttonManager != null && !buttonManager.isInteracted)
                {
                    buttonManager.isInteracted = true;
                    buttonManager.Interacted();
                }
                else
                {
                    raycastTransform = null;
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
}
