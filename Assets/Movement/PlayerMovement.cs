using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour {
    Camera cam;
    NavMeshAgent navMeshAgent;
    [SerializeField] LayerMask ground;
    [SerializeField] float speedAnimationMult = 0.2f;
    private EventInstance movePenSFX;
    Animator animator;
    bool lockInput = false;
    void Awake() {
        cam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable() {
        //navMeshAgent.autoTraverseOffMeshLink = true;
    }

    private void Start() {
        movePenSFX = AudioManager.instance.CreateInstance(FMODEvents.instance.penMovement);
    }

    void Update() {
        UpdateSound();
        if (!lockInput) animator.speed = navMeshAgent.velocity.magnitude * speedAnimationMult;
        if (GameMGR.Instance.CurrentState == GameState.Fight) {
            movePenSFX.stop(STOP_MODE.IMMEDIATE);
        }
    }
    public void GetItem(Vector3 pos) {
        navMeshAgent.destination = pos;
        lockInput = true;
        animator.SetTrigger("GetItem");
    }
    public void JumpIn(Vector3 pos, Vector3 destinationPos) {
        navMeshAgent.destination = pos;
        lockInput = true;
        animator.SetTrigger("JumpIn");
        if(destinationPos != Vector3.zero) StartCoroutine(WaitForJumpOut(destinationPos));
    }
    public void JumpOut(Vector3 pos) {
        navMeshAgent.enabled = false;
        transform.position = pos;
        navMeshAgent.enabled = true;
        navMeshAgent.destination = pos;
        animator.SetTrigger("JumpOut");
    }

    IEnumerator WaitForJumpOut(Vector3 whereToGo) {
        while (!PageManager.Instance.AreAllUp() && PageManager.Instance.InTransition) {
            yield return null;
        }
        JumpOut(whereToGo);
    }
    private void FixedUpdate() {

    }
    public void UnLockInput() {
        lockInput = false;
    }
    private void OnMouse() {
        if (!lockInput && Mouse.current.leftButton.wasPressedThisFrame) {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, ground)) {
                NavMeshPath navMeshPath = new NavMeshPath();
                if (navMeshAgent.CalculatePath(hit.point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) {
                    navMeshAgent.destination = hit.point;
                }

            }
        }
    }

    private void UpdateSound() {
        if (GameMGR.Instance.CurrentState == GameState.World) {
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
                PLAYBACK_STATE playBackState;
                movePenSFX.getPlaybackState(out playBackState);
                if (playBackState.Equals(PLAYBACK_STATE.STOPPED)) {
                    movePenSFX.start();
                }
            } else {
                movePenSFX.stop(STOP_MODE.IMMEDIATE);
            }
        }

    }
}
