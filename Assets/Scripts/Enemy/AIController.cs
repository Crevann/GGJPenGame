using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float startWaitTime = 4;
    public float timeToRotation = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeResolveIterations = 4;
    public float edgeDstThreshold = 0.5f;

    public Transform[] wayPoints;
    int CurrentwayPointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 playerPosition;

    float waitTime;
    float timeToRotate;
    bool playerInRange;
    bool playerNear;
    bool enemyPatrol;
    bool caughtPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = Vector3.zero;
        enemyPatrol = true;
        caughtPlayer = false;
        playerInRange = false;
        waitTime = startWaitTime;
        timeToRotate = timeToRotation;

        CurrentwayPointIndex = 0;
        agent = GetComponent<NavMeshAgent>();

        agent.isStopped = false;
        agent.speed = speedWalk;
        agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnviromentView();

        if (!enemyPatrol)
        {
            ChaseEnemy();
        }
        else
        {
            EnemyPatrol();
        }
    }

    private void ChaseEnemy()
    {
        playerNear = false;
        playerLastPosition = Vector3.zero;

        if (!caughtPlayer)
        {
            EnemyMove(speedRun);
            agent.SetDestination(playerPosition);
        }
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if(waitTime <= 0 && caughtPlayer && Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                enemyPatrol = true;
                playerNear = false;
                EnemyMove(speedWalk);
                timeToRotate = timeToRotation;
                waitTime = startWaitTime;
                agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
            }
            else
            {
                if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    StopEnemy();
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void EnemyPatrol()
    {
        if (playerNear)
        {
            if(timeToRotate <= 0)
            {
                EnemyMove(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                StopEnemy();
                timeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            playerNear = false;
            playerLastPosition = Vector3.zero;
            agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                if(waitTime <= 0)
                {
                    NextPoint();
                    EnemyMove(speedWalk);
                    waitTime = startWaitTime;
                }
                else
                {
                    StopEnemy();
                    timeToRotate -= Time.deltaTime;
                }
            }
        }
    }

    void EnemyMove(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }

    void StopEnemy()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    public void NextPoint()
    {
        CurrentwayPointIndex = (CurrentwayPointIndex + 1) % wayPoints.Length;
        agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
    }

    void CaughtPlayer() 
    {
        caughtPlayer = true;
    }
    
    void LookingPlayer(Vector3 player)
    {
        agent.SetDestination(player);
        if(Vector3.Distance(transform.position,player) <= 0.3)
        {
            if(waitTime <= 0)
            {
                playerNear = false;
                EnemyMove(speedWalk);
                agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
                waitTime = startWaitTime;
                timeToRotate = timeToRotation;
            }
            else
            {
                StopEnemy();
                waitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInViewRange = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < playerInViewRange.Length; i++)
        {
            Transform player = playerInViewRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    playerInRange = true;
                    enemyPatrol = false;
                }
                else
                {
                    playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                playerInRange = false;
            }
            if (playerInRange)
            {
                playerPosition = player.transform.position;
            }
        }
    }
}
