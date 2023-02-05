using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Tutorial : Singleton<Tutorial>
{
    private enum State { LearnToMove, LearnToPickUp, OpenInventory, GoToFight, OpenDoor, JumpInTheWhole}
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
    [SerializeField] UnityEvent openedDoor;
    [SerializeField] UnityEvent jumped;

    bool inventoryOpened;


    private State currentState = State.LearnToMove;

    PlayerMovement player;


    bool fighting;
    float counter;
    bool isRising;
    bool doorOpened;
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
        if (!fioreAgent.enabled || !fioreAgent) return;
        if (GameMGR.Instance.CurrentState == GameState.Fight && !fighting) {
            fighting = true;
            currentState = State.OpenDoor;
            fought.Invoke();
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
                if(Inventory.Instance.roots[1] != null) {
                    currentState = State.OpenDoor;
                    fought.Invoke();
                }
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
            case State.OpenDoor:
                if (doorOpened) {
                    currentState = State.JumpInTheWhole;
                    openedDoor.Invoke();
                }
                break;
            default:
                break;
        }

    }
    public void InventoryOpened() {
        inventoryOpened = true;
    }
    public void DoorOpened() {
        doorOpened = true;
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
