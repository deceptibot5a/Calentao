using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaAnimationManager : MonoBehaviour

{
    public Animator animator;
    public Vector3 lastPosition;
    public GameObject camera;
    public GameObject cameraStandPosition;
    public GameObject cameraFallingPosition;
    public GuiaPlayerController playerController;
    
    public float smoothness = 10;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = this.transform.position;
        playerController = GetComponentInParent<GuiaPlayerController>();
    }

     void Update()
    {
        if (!playerController.CantWalk)
        {
            if (playerController.IsFalling)
            {
                animator.SetBool("IsFalling", true);
                camera.transform.position = cameraFallingPosition.transform.position;
            }
            else
            {
                animator.SetBool("IsFalling", false);
                camera.transform.position = cameraStandPosition.transform.position;
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                // Calcula la velocidad en función de si está corriendo o caminando
                float speed = 0f;
                if (playerController.CanRun && Input.GetKey(KeyCode.LeftShift))
                {
                    speed = playerController.RunSpeed;
                }
                else
                {
                    speed = playerController.WalkSpeed;
                }

                animator.SetFloat("Speed", speed);

                // Detecta si está corriendo o caminando y establece las animaciones correspondientes
                if (speed > 0)
                {
                    if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
                    {
                        if (speed == playerController.RunSpeed)
                        {
                            // Corriendo
                            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
                            {
                                if (horizontal > 0)
                                {
                                    
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), 2, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 0, Time.deltaTime * smoothness));
                                }
                                else
                                {
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), -2, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 0, Time.deltaTime * smoothness));
                 
                                }
                            }
                            else
                            {
                                if (vertical > 0)
                                {
                                    // Correr hacia adelante
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), 0, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 2, Time.deltaTime * smoothness));
                                }
                                else
                                {
                                    // Correr hacia atrás
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), 0, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), -2, Time.deltaTime * smoothness));
                                }
                            }
                        }
                        else
                        {
                            // Caminando
                            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
                            {
                                if (horizontal > 0)
                                {
                                    // Caminar hacia la derecha
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), 1, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 0, Time.deltaTime * smoothness));
                                }
                                else
                                {
                                    // Caminar hacia la izquierda
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), -1, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 0, Time.deltaTime * smoothness));
                                }
                            }
                            else
                            {
                                if (vertical > 0)
                                {
                                    // Caminar hacia adelante
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), 0, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 1, Time.deltaTime * smoothness));
                                }
                                else
                                {
                                    // Caminar hacia atrás
                                    animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), 0, Time.deltaTime * smoothness));
                                    animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), -1, Time.deltaTime * smoothness));
                                }
                            }
                        }
                    }
                    else
                    {
                        // El jugador no se está moviendo
                        animator.SetFloat("Horizontal", Mathf.Lerp(animator.GetFloat("Horizontal"), 0, Time.deltaTime * smoothness));
                        animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 0, Time.deltaTime * smoothness));
                    }
                }
            }
        }
    }
}