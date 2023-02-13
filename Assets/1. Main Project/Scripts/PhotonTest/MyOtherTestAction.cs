using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOtherTestAction : MonoBehaviour
{
    private Vector3 rotation = new Vector3(0, 0, 1);
    private float speed = 10f;

    void Update() {
        if (StateController.secondIsActive == true) {
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
    }
}
