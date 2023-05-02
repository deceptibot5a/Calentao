using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public EnemyController enemyController; // Asigna el objeto EnemyController en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            enemyController.chasingPlayer = false; // Detiene la persecución del jugador
            enemyController.playerInSafeZone = true; // Establece que el jugador está en una zona segura
            enemyController.GoToNearestPatrolPoint(); // Manda al enemigo a patrullar al punto de patrullaje más cercano
            Debug.Log("Player in safe zone");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            enemyController.playerInSafeZone = false; // Establece que el jugador ya no está en una zona segura
            Debug.Log("Player left safe zone");
        }
    }
}

