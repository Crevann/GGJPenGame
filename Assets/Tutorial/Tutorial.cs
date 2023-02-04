using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Tutorial : Singleton<Tutorial>
{
    private enum State { LearnToMove, LearnToPickUp, OpenInventory, GoToFight, JumpInTheWhole}
    [SerializeField] TeleportingObject CaneFiore;
    [SerializeField] AnimationCurve standUpMovement;
    [SerializeField] float timeToRaise = 1;
    [SerializeField] float fadeTime = 1;
    [SerializeField] Transform[] fiorePositions;

    [Header("Events:")]
    bool done = false;
    [SerializeField] UnityEvent startTutorial;
    [SerializeField] UnityEvent moved;
    [SerializeField] UnityEvent pickedUp;
    [SerializeField] UnityEvent openedInventory;
    [SerializeField] UnityEvent fought;
    [SerializeField] UnityEvent jumped;

    bool inventoryOpened;


    private State currentState = State.LearnToMove;

    PlayerMovement player;


    bool fighting;
    float counter;
    bool isRising;
    float risingVal = 0;
    NavMeshAgent fioreAgent;
    NavMeshAgent penAgent;


    private void Start() {
        fioreAgent = CaneFiore.GetComponent<NavMeshAgent>();
        fioreAgent.enabled = false;
        fighting = false;
    }
    void Update()
    {
        if (GameMGR.Instance.CurrentState == GameState.Fight) {
            fighting = true;
            currentState = State.JumpInTheWhole;
        }
        if (!player || !player.isActiveAndEnabled) {
            player = FindObjectOfType<PlayerMovement>();
            if(player) penAgent = player.GetComponent<NavMeshAgent>();
            done = false;
            return;
        }
        Rise();
        Do();
    }
    void Do() {
        if (risingVal < 1) return;
        fioreAgent.destination = fiorePositions[(int)currentState].position;
        switch (currentState) {
            case State.LearnToMove:
                if (!done) {
                    startTutorial.Invoke();
                    done = true;
                }
                if (penAgent.velocity.sqrMagnitude > 0) {
                    done = false;
                    currentState = State.LearnToPickUp;
                    moved.Invoke();
                }
                break;
            case State.LearnToPickUp:
                if (Inventory.Instance.roots[0] != null) {
                    currentState = State.OpenInventory;
                    pickedUp.Invoke();
                }
                break;
            case State.GoToFight:
                //if(fighting) {
                //    currentState = State.JumpInTheWhole;
                //    Debug.Log("JumpInDaHole");
                //    fought.Invoke();
                //}
                break;
            case State.JumpInTheWhole:
                break;
            case State.OpenInventory:
                if(inventoryOpened) {
                    currentState = State.GoToFight;
                    openedInventory.Invoke();
                }
                break;
            default:
                break;
        }

    }
    public void InventoryOpened() {
        inventoryOpened = true;
    }
    void Rise() {
        if (risingVal < 1 && CaneFiore.onTopOfBook) {
            if (!isRising) {
                fioreAgent.enabled = true;
                counter = 0;
                isRising = true;
            }
            counter += Time.deltaTime;
            risingVal = counter / timeToRaise;
            risingVal = Mathf.Clamp01(risingVal);
            CaneFiore.transform.GetChild(0).rotation = Quaternion.Euler(Vector3.right * standUpMovement.Evaluate(risingVal) * 90);
            if (!fioreAgent.enabled && risingVal >= 1) {
                //CaneFiore.transform.GetChild(0).rotat = Quaternion.Euler(Vector3.right * 90);
            }
        }

    }
}
