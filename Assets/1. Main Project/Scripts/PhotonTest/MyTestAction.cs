using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTestAction : MonoBehaviour
{
    private Vector3 rotation = new Vector3(0,0,1);
    private float speed = 10f;

    void Update() {
        if (StateController.firstIsActive == true) {
            transform.Rotate(rotation * speed * Time.deltaTime);
        }
    }
}
