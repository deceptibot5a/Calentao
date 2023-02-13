using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherTriggerTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StateController.firstIsActive = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StateController.firstIsActive = false;
        }
    }
}
