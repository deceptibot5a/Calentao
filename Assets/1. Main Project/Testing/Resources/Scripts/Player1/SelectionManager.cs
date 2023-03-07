using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float distance = 3f;
    private Transform highlight;
    private Transform selection;
    public ButtonPuzzle puzzle;

    // Variable booleana que indica si el objeto ya ha sido seleccionado.
    private bool alreadySelected = false;

    void Update()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
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

        Mouse mouse = InputSystem.GetDevice<Mouse>();
        if (highlight != null && mouse.leftButton.wasPressedThisFrame && !alreadySelected)
        {
            selection = highlight;
            //Debug.Log("Selected: " + selection.name);
            if (selection.gameObject.GetComponent<ButtonManager>() != null)
            {
                selection.gameObject.GetComponent<ButtonManager>().Interacted();
                //Debug.Log("presione el boton ");
            }

            // Establece alreadySelected en verdadero para indicar que el objeto ya ha sido seleccionado.
            alreadySelected = true;
        }
        else if (mouse.leftButton.wasPressedThisFrame)
        {
            selection = null;
        }

        // Si el botón izquierdo del mouse se suelta, establece alreadySelected en falso para permitir que otro jugador seleccione el objeto.
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            alreadySelected = false;
        }
    }
}
