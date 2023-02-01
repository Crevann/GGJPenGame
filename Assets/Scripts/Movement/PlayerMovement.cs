using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent navMeshAgent;
    [SerializeField] LayerMask ground;
    void Awake()
    {
        cam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
    }
    private void OnMouse() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000,ground)) {
                NavMeshPath navMeshPath = new NavMeshPath();
                if (navMeshAgent.CalculatePath(hit.point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                    navMeshAgent.destination = hit.point;
            }
        }
    }
}
