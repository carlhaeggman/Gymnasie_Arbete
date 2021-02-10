using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float timeBetweenAttacks, attackRange, rotationSpeed;
    bool alreadyAttacked, playerInRange;
    public GameObject projectile;
    public Transform shootPos;

    private Quaternion _lookRotation;
    private Vector3 _direction;
    public float health;

    RaycastHit hit;

    private GameObject[] spawnPoints;
    public Transform closestSpawnPoint;
    public bool outOfSpawn;
    private bool clearedSpawn;
    


    public GameObject getClosestOutOfSpawnPoint()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("OutOfSpawnPosTag");
        float closestDistance = Mathf.Infinity;
        GameObject targetPoint = null;
        
        foreach (GameObject point in spawnPoints)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, point.transform.position);
            if(currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                targetPoint = point;
            }
        }
        return targetPoint;
    }

    private void Awake()
    {
        clearedSpawn = false;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        getClosestOutOfSpawnPoint();
        agent.SetDestination(getClosestOutOfSpawnPoint().transform.position);
    }
    private void Update()
    {
        if(outOfSpawn == false)
        {
            GetOutOfSpawn();
        }
        else
        {
            playerInRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (Physics.Raycast(shootPos.position, (player.position - transform.position), out hit, attackRange) && hit.transform.CompareTag("Player"))
            {
                if (outOfSpawn == true)
                {
                    agent.enabled = false;
                    if (hit.transform.CompareTag("Player"))
                    {
                        AttackPlayer();
                    }
                }
            }
            else
            {
                if (agent.enabled == false)
                {
                    agent.enabled = true;
                }
                ChasePlayer();
            }
        }
    }
    private void ChasePlayer()
    {
        if(outOfSpawn == true)
        {
            agent.SetDestination(player.position);
        }
    }
    private void AttackPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (!alreadyAttacked)
        {
            Rigidbody projectileRB = Instantiate(projectile, shootPos.position, transform.rotation).GetComponent<Rigidbody>();
            projectileRB.AddForce(transform.forward * 50f, ForceMode.Impulse);
            projectileRB.AddForce(transform.up * 5f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void GetOutOfSpawn()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance + 3)
            {
                outOfSpawn = true;
            }
        }
    }

   
}
