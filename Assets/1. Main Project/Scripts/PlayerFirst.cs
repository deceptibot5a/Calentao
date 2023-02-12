using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFirst : MonoBehaviourPunCallbacks 
{
    void Update() {
        if (photonView.IsMine) {
            float x = Input.GetAxis("Horizontal") * 10f * Time.deltaTime;
            float z = Input.GetAxis("Vertical") * 10f * Time.deltaTime;
            transform.Translate(x, 0, z);
        }
    }
}
