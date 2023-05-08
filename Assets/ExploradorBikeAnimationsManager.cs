using System.Collections;
using System.Collections.Generic;
using AlekGames.HoverCraftSystem.Systems.Addons;
using AlekGames.HoverCraftSystem.Systems.Main;
using UnityEngine;

public class ExploradorBikeAnimationsManager : MonoBehaviour
{
    public Transform LeftMediumTransform;
    public Transform LeftFinalTransform;
    public Transform CameraFinalPosition;
    public Transform CameraMediumPosition;
    public GameObject playerMesh;
    public GameObject BikeCamera;
    public hoverCraft hoverCraft;
    public hoverCraftTilt hoverCraftTilt; 
    
    public float cameraInterpolationSpeed = 2f;
    

    
    public void TransformCameraMediumPosition()
    {
        BikeCamera.transform.position = CameraMediumPosition.position; 
        BikeCamera.transform.rotation = CameraMediumPosition.rotation;
    }
    public void BikeLeftFinalTransform()
    {
        StartCoroutine(DoBikeLeftFinalTransform());
    }

    private IEnumerator DoBikeLeftFinalTransform()
    {
        yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos antes de hacer la transformación

        playerMesh.transform.position = LeftFinalTransform.position;
        playerMesh.transform.rotation = LeftFinalTransform.rotation;

        float t = 0.0f; // Factor de interpolación inicial
        while (t < 1.0f) // Mientras no se haya completado la interpolación
        {
            t += Time.deltaTime * cameraInterpolationSpeed; // Incrementa el factor de interpolación en función del tiempo y la velocidad de interpolación

            // Interpola la posición y la rotación de la cámara
            BikeCamera.transform.position = Vector3.Lerp(CameraMediumPosition.position, CameraFinalPosition.position, t);
            BikeCamera.transform.rotation = Quaternion.Lerp(CameraMediumPosition.rotation, CameraFinalPosition.rotation, t);

            yield return null; // Espera al siguiente frame antes de continuar
        }

        hoverCraft.enabled = true;
        hoverCraftTilt.enabled = true;
    }
    
    public void BikeLeftMediumTransform()
    {
        StartCoroutine(DoBikeLeftMediumTransform());
    }
    
    private IEnumerator DoBikeLeftMediumTransform()
    {
        yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos antes de hacer la transformación
        playerMesh.transform.position = LeftMediumTransform.position; 
        playerMesh.transform.rotation = LeftMediumTransform.rotation;
    }
    
   
}
