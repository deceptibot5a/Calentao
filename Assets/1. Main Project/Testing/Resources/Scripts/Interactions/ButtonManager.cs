using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    public int buttonNumber;
    public ButtonPuzzle puzzleController;
    //Animator animator;
    //public Animator luz;

    void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        //luz = gameObject.GetComponent<Animator>();
    }

    public void Interacted()
    {
        puzzleController.ButtonPressed(buttonNumber);
        //animator.SetTrigger("pressed");
        //luz.SetBool("light", true);
    }
}
