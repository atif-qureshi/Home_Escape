using UnityEngine;
using UnityEngine.AI;

public class RandomPatrolEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float chaseRange = 10f;
    public AudioSource alertSound;
    public float patrolRadius = 10f; // room ke andar random points

    private bool chasing = false;
    private bool soundPlayed = false;
    private Vector3 patrolPoint;

    void Start()
    {
        SetRandomPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            // Player detected → chase
            agent.SetDestination(player.position);
            chasing = true;

            if (!soundPlayed && alertSound != null)
            {
                alertSound.Play();
                soundPlayed = true;
            }
        }
        else
        {
            // Player lost → random patrol
            if (chasing)
            {
                chasing = false;
                soundPlayed = false;
                SetRandomPatrolPoint();
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f && !chasing)
            {
                SetRandomPatrolPoint();
            }
        }
    }

    void SetRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            agent.SetDestination(patrolPoint);
        }
    }
}
