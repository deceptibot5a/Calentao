using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; 
    public float patrolSpeed = 2f; 
    public float chaseSpeed = 4f;
    public Transform checkpoint;
    private int currentPatrolIndex = 0; 
    private NavMeshAgent agent; 
    private bool playerInRange = false;
    public float TPdelayTime = 0.42f;
    public PhotonView photonView;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetPatrolDestination();
        agent.speed = patrolSpeed;
    }

    private void Update()
    {
        if (playerInRange)
        {
            PlayerDetected();
        }
        else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            SetPatrolDestination();
        }
    }

    private void SetPatrolDestination()
    {
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            playerInRange = true;
            agent.speed = chaseSpeed;
            agent.SetDestination(other.transform.position);
            PlayerDetected();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            playerInRange = false;
            agent.speed = patrolSpeed;
            SetPatrolDestination();
        }
    }

    private void PlayerDetected()
    {
        StartCoroutine(Checkpoints.instance.FadeInAndOut());
        Invoke("TeleportPlayer", TPdelayTime);
        Debug.Log("Player detected!");
    }

    
    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player1");
        if (player != null)
        {
            player.transform.position = checkpoint.transform.position;
        }
    }
}
