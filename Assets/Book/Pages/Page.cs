using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Page : MonoBehaviour {
    [HideInInspector] public TeleportingObject[] teleportingObjects;
    [SerializeField] private bool allDown;
    RiseUpAndDown[] risingObjects;
    NavMeshAgent[] navMeshAgents;
    [SerializeField] public BoxCollider collider;
    [SerializeField] bool goDown;
    void Start() {
        teleportingObjects = transform.GetComponentsInChildren<TeleportingObject>();
        risingObjects = transform.GetComponentsInChildren<RiseUpAndDown>();
        navMeshAgents = transform.GetComponentsInChildren<NavMeshAgent>();
    }
    public void DeactivateAll() {
        foreach (NavMeshAgent item in navMeshAgents) {
            item.enabled = false;
        }
        foreach (RiseUpAndDown item in risingObjects) {
            item.GoDown();
        }
    }
    public bool AreAllDown() {
        allDown = true;
        foreach (RiseUpAndDown item in risingObjects) {
            if (item.CurrentState != RiseUpAndDown.State.Lowered) {
                allDown = false;
                break;
            }
        }
        return allDown;
    }
    public bool AreAllUp() {
        bool up = true;
        foreach (RiseUpAndDown item in risingObjects) {
            if (item.RiseMethod == RiseUpAndDown.RisingMethod.OnPageOpen && item.CurrentState != RiseUpAndDown.State.Risen) {
                up = false;
                break;
            }
        }
        return up;
    }
    void Update() {
        if (goDown) { 
            goDown = false;
            DeactivateAll();
        }
        AreAllDown();
    }
}
