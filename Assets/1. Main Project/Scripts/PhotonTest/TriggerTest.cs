using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StateController.secondIsActive = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StateController.secondIsActive = false;
        }
    }
}
