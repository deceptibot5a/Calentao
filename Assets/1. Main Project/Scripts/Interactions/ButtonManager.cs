using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    public int buttonNumber;
    public ButtonPuzzle puzzleController;
    public bool isInteracted = false;
    public PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void Interacted()
    {
        puzzleController.ButtonPressed(buttonNumber);
        isInteracted = true;
    }
}
