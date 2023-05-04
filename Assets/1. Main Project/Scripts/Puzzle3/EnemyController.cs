using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseRange = 10f;
    public float patrolPointRange = 3f;
    public int currentPatrolIndex = 0;
    public NavMeshAgent navAgent;
    public GameObject player;
    public bool chasingPlayer = false;
    public bool  playerInSafeZone = false;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(AssingPLayer1());
        navAgent.speed = patrolSpeed;
        GoToNextPatrolPoint();
    }

    private void Update()
    {
        
        Debug.DrawRay(transform.position, transform.forward * chaseRange,Color.red);
        if (playerInSafeZone)
        {
            chasingPlayer = false;
            navAgent.speed = patrolSpeed;
            GoToNearestPatrolPoint();
        }
        if (!chasingPlayer && !playerInSafeZone)
        {
            Patrol();
            Debug.DrawRay(transform.position, transform.forward * chaseRange,Color.red);
            
        }
        else if (chasingPlayer && !playerInSafeZone)
        {
            ChasePlayer();
        }
    }

    public void Patrol()
    {
        if (navAgent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < chaseRange && !playerInSafeZone)
        {
            chasingPlayer = true;
            navAgent.speed = chaseSpeed;
        }
    }

    private void GoToNextPatrolPoint()
    {
        navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(player.transform.position);

        Debug.DrawRay(transform.position, navAgent.velocity.normalized * 3f, Color.red);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > chaseRange)
        {
            chasingPlayer = false;
            navAgent.speed = patrolSpeed;
            GoToNearestPatrolPoint(); 
        }

        float distanceToClosestPatrolPoint = Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position);
        
        if (distanceToClosestPatrolPoint < patrolPointRange && !playerInSafeZone)
        {
            chasingPlayer = false;
            navAgent.speed = patrolSpeed;
        }
    }

    public void GoToNearestPatrolPoint()
    {
        float minDistance = Mathf.Infinity;
        int minIndex = 0;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, patrolPoints[i].position);

            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        currentPatrolIndex = minIndex;
        navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }
    
    IEnumerator AssingPLayer1()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindGameObjectWithTag("Player1");
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}