using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent navMeshAgent;
    [SerializeField] LayerMask ground;
    [SerializeField] float speedAnimationMult = 0.2f;
    private EventInstance movePenSFX;
    Animator animator;
    void Awake()
    {
        cam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        movePenSFX = AudioManager.instance.CreateInstance(FMODEvents.instance.penMovement);
    }

    void Update()
    {
        UpdateSound();
        animator.speed = navMeshAgent.velocity.magnitude * speedAnimationMult;
    }

    private void FixedUpdate()
    {
        
    }
    private void OnMouse() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000,ground)) {
                NavMeshPath navMeshPath = new NavMeshPath();
                if (navMeshAgent.CalculatePath(hit.point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    navMeshAgent.destination = hit.point;
                }
                    
            }
        }
    }

    private void UpdateSound()
    {
        if(navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            PLAYBACK_STATE playBackState;
            movePenSFX.getPlaybackState(out playBackState);
            if (playBackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                movePenSFX.start();
            }
        }
        else
        {
            movePenSFX.stop(STOP_MODE.IMMEDIATE);
        }
    }
}
