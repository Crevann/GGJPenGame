using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(TeleportingObject), typeof(NavMeshAgent))]
public class AIController : MonoBehaviour {
    [SerializeField] float startWaitTime = 3;
    [SerializeField] float timeToRotation = 2;
    [SerializeField] float speedWalk = 6;
    [SerializeField] float speedRun = 9;

    [SerializeField] bool advancedView = false;
    [SerializeField] float viewRadius = 15;
    [SerializeField] float viewAngle = 90;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] float meshResolution = 1f;
    [SerializeField] int edgeResolveIterations = 4;
    [SerializeField] float edgeDstThreshold = 0.5f;

    [SerializeField] Transform[] wayPoints;
    int CurrentwayPointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 playerPosition;

    NavMeshAgent agent;

    float waitTime;
    float timeToRotate;
    bool playerInRange;
    bool playerNear;
    [SerializeField] bool enemyPatrol;
    bool caughtPlayer;

    #region ale
    PlayerMovement player;

    [Header("Standing Up")]
    [SerializeField] AnimationCurve standUpMovement;
    [SerializeField] float timeToRaise = 1;
    [SerializeField] float timeToFall = 1;
    [SerializeField] float distanceFromPlayer = 30;
    TeleportingObject myTeleportingObject;
    [HideInInspector] public bool isRising = false;
    Quaternion myCurrentRotation;
    float counter;
    bool readyToMove = false;
    bool wasReadyToMove = false;
    Vector2 radiusAndHeight;
    SwitchFaceEnemy switchFace;
    #endregion
    // Start is called before the first frame update
    void Start() {
        playerPosition = Vector3.zero;
        enemyPatrol = true;
        caughtPlayer = false;
        playerInRange = false;
        waitTime = startWaitTime;
        timeToRotate = timeToRotation;


        CurrentwayPointIndex = 0;
        agent = GetComponent<NavMeshAgent>();

        radiusAndHeight = new Vector2(agent.radius, agent.height);
        //agent.isStopped = true;
        agent.speed = speedWalk;
        agent.enabled = false;
        //agent.SetDestination(wayPoints[CurrentwayPointIndex].position);

        myTeleportingObject = GetComponent<TeleportingObject>();
        switchFace = GetComponent<SwitchFaceEnemy>();
    }

    // Update is called once per frame
    void Update() {
        if (!myTeleportingObject.onTopOfBook) {
            if (agent.enabled) agent.enabled = false;

            return;
        }
        if (agent.enabled && agent.isOnOffMeshLink) {
            agent.CompleteOffMeshLink();
        }
        if (readyToMove) {
            if (!wasReadyToMove) {
                agent.radius = radiusAndHeight.x / transform.localScale.x;
                agent.height = radiusAndHeight.y / transform.localScale.y;
                wasReadyToMove = true;
                agent.enabled = true;
                agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
            }
            EnviromentView();
            if (agent.enabled) {
                if (enemyPatrol) {
                    EnemyPatrol();
                } else {
                    ChasePlayer();
                }
            }

        }
        RisingAndLowering();

        if (!readyToMove) wasReadyToMove = false;
        if (switchFace) switchFace.enabled = readyToMove;
    }
    void RisingAndLowering() {
        if (!player || !player.isActiveAndEnabled) {
            player = FindObjectOfType<PlayerMovement>();
            return;
        }
        if (distanceFromPlayer * distanceFromPlayer >= (transform.position - player.transform.position).sqrMagnitude) {
            if (!isRising) {
                counter = 0;
                isRising = true;
            }
            counter += Time.deltaTime;
            float c = counter / timeToRaise;
            c = Mathf.Clamp01(c);
            transform.rotation = Quaternion.Euler(Vector3.right * standUpMovement.Evaluate(c) * 90);
            readyToMove = c == 1;
        } else {
            readyToMove = false;
            if (isRising) {
                counter = 0;
                isRising = false;

                myCurrentRotation = transform.rotation;
            }
            counter += Time.deltaTime;
            float c = counter / timeToRaise;
            c = Mathf.Clamp01(c);
            transform.rotation = Quaternion.Lerp(myCurrentRotation, Quaternion.Euler(Vector3.right * 90), c);

        }
    }

    private void ChasePlayer() {
        if (!advancedView) {
            EnemyMove(speedRun);
            agent.SetDestination(playerPosition);
            return;
        }




        playerNear = false;
        playerLastPosition = Vector3.zero;

        if (!caughtPlayer) {
            EnemyMove(speedRun);
            agent.SetDestination(playerPosition);
        }
        if (agent.remainingDistance <= agent.stoppingDistance) {
            if (waitTime <= 0 && caughtPlayer && Vector3.Distance(transform.position, player.transform.position) >= 5f) {
                enemyPatrol = true;
                playerNear = false;
                EnemyMove(speedWalk);
                timeToRotate = timeToRotation;
                waitTime = startWaitTime;
                agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
            } else {
                if (Vector3.Distance(transform.position, player.transform.position) >= 1.5f) {
                    StopEnemy();
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void EnemyPatrol() {
        if (!advancedView) {
            if (!agent.enabled) {
                //agent.enabled = true;
                EnemyMove(speedWalk);
            }
            agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)) {
                if (waitTime <= 0) {
                    NextPoint();
                    EnemyMove(speedWalk);
                    waitTime = startWaitTime;
                } else {
                    StopEnemy();
                    waitTime -= Time.deltaTime;
                }
            }
            return;
        }

        if (playerNear) {
            if (timeToRotate <= 0) {
                EnemyMove(speedWalk);
                LookingPlayer(playerLastPosition);
            } else {
                StopEnemy();
                timeToRotate -= Time.deltaTime;
            }
        } else {
            playerNear = false;
            playerLastPosition = Vector3.zero;
            agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (waitTime <= 0) {
                    NextPoint();
                    EnemyMove(speedWalk);
                    waitTime = startWaitTime;
                } else {
                    //StopEnemy();
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    void EnemyMove(float speed) {
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        agent.speed = speed;
    }

    void StopEnemy() {
        agent.isStopped = true;
        agent.speed = 0;
    }

    public void NextPoint() {
        CurrentwayPointIndex = (CurrentwayPointIndex + 1) % wayPoints.Length;
        agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
    }

    void CaughtPlayer() {
        caughtPlayer = true;
    }

    void LookingPlayer(Vector3 player) {
        agent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3) {
            if (waitTime <= 0) {
                playerNear = false;
                EnemyMove(speedWalk);
                agent.SetDestination(wayPoints[CurrentwayPointIndex].position);
                waitTime = startWaitTime;
                timeToRotate = timeToRotation;
            } else {
                StopEnemy();
                waitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView() {
        Collider[] playerInViewRange = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (!advancedView) {
            NavMeshPath path = new NavMeshPath();
            playerInRange = playerInViewRange.Length > 0 && agent.CalculatePath(player.transform.position, path) && !player.GetComponent<NavMeshAgent>().isOnOffMeshLink && !agent.isOnOffMeshLink;
            playerPosition = player.transform.position;
            enemyPatrol = !playerInRange;
            //if (enemyPatrol) agent.enabled = false;
            return;
        }
        for (int i = 0; i < playerInViewRange.Length; i++) {
            Transform player = playerInViewRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2) {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask)) {
                    playerInRange = true;
                    enemyPatrol = false;
                } else {
                    playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius) {
                playerInRange = false;
            }
            if (playerInRange) {
                playerPosition = player.transform.position;
            }
        }
    }
}
