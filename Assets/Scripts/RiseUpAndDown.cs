using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RiseUpAndDown : MonoBehaviour
{
    enum RisingMethod { OnPageOpen, OnPlayerNear, OnlyOnEvent }
    public enum State { Lowered, Risen, Moving}
    private float riseAmount = 0; //0 - 1
    public float RiseAmount => riseAmount;

    [SerializeField] float timeToRaise = 1;
    [SerializeField] float timeToFall = 1;
    [SerializeField] AnimationCurve standUpMovement;

    [SerializeField] RisingMethod riseMethod = RisingMethod.OnPageOpen;

    [SerializeField] State currentState = State.Lowered;
    public State CurrentState => currentState;

    [Header("Only for OnPlayerNear")]
    [SerializeField] float distanceFromPlayer = 5;

    [SerializeField] TeleportingObject teleportingObject;
    PlayerMovement player;

    [Header("Events")]
    [SerializeField] UnityEvent OnRise;
    [SerializeField] UnityEvent OnLowered;
    Quaternion myCurrentRotation;
    bool isRising;
    float counter;
    void Start()
    {
        currentState = State.Lowered;
        if (riseMethod == RisingMethod.OnPageOpen && !teleportingObject) teleportingObject = GetComponent<TeleportingObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (riseMethod == RisingMethod.OnPageOpen && teleportingObject != null && teleportingObject.onTopOfBook) isRising = true;
        else if (riseMethod == RisingMethod.OnPageOpen && teleportingObject != null && !teleportingObject.onTopOfBook) return;

        if (riseMethod == RisingMethod.OnPlayerNear && (!player || !player.isActiveAndEnabled)) {
            player = FindObjectOfType<PlayerMovement>();
            return;
        }else if (riseMethod == RisingMethod.OnPlayerNear) {
            isRising = distanceFromPlayer * distanceFromPlayer >= (transform.position - player.transform.position).sqrMagnitude;
        }

        RisingAndLowering();
    }
    void RisingAndLowering() {

        if (isRising && currentState != State.Risen) {
            if(currentState != State.Moving) {
                counter = 0;
                currentState = State.Moving;
            }
            counter += Time.deltaTime;
            float c = counter / timeToRaise;
            c = Mathf.Clamp01(c);
            transform.localRotation = Quaternion.Euler(Vector3.right * standUpMovement.Evaluate(c) * 90);
            if (c == 1) {
                currentState = State.Risen;
                OnRise.Invoke();
            }
        }
        if (!isRising && currentState != State.Lowered) {
            if(currentState != State.Moving) {
                counter = 0;
                myCurrentRotation = transform.localRotation;
                currentState = State.Moving;
            }
            counter += Time.deltaTime;
            float c = counter / timeToFall;
            c = Mathf.Clamp01(c);
            transform.localRotation = Quaternion.Lerp(myCurrentRotation, Quaternion.Euler(Vector3.right * 90), c);
            if (c == 1) {
                currentState = State.Risen;
                OnRise.Invoke();
            }
        }
        if(currentState == State.Lowered)
            transform.localRotation = Quaternion.Euler(Vector3.right * 90);



    }
    public void RiseUp() {
        isRising = true;
    }
    public void GoDown() {
        isRising = false;
    }
}
