using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; 
    public float patrolSpeed = 2f; 
    public float chaseSpeed = 4f; 
    public CanvasGroup deathPanel;
    public Transform checkpoint;
    public Transform playerTransform;
    private int currentPatrolIndex = 0; 
    private NavMeshAgent agent; 
    private bool playerInRange = false;
    

    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetPatrolDestination();
        agent.speed = patrolSpeed;
        StartCoroutine(GetPlayerTransform());
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

    private void OnTriggerStay(Collider other)
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
        LeanTween.alphaCanvas(deathPanel, 1f, 0.3f).setOnComplete(TurnOffDeathPanel);
        playerTransform.position = checkpoint.position;
        Debug.Log("Player detected!");
    }
    public void TurnOffDeathPanel()
    {
        LeanTween.alphaCanvas(deathPanel, 0f, 0.6f);
    }
    IEnumerator GetPlayerTransform()
    {
        yield return new WaitForSeconds(0.01f);
        playerTransform = GameObject.FindGameObjectWithTag("Player1").transform;
    }
}
