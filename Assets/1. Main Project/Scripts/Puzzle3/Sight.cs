using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    EnemyController enemyController;
    Renderer renderer;

    void Awake() {
        enemyController = GetComponentInParent<EnemyController>();
        renderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player1")) {
            //enemyController.PlayerSpoted(other.transform);
            renderer.material.color = Color.red;
        }
    }
}
