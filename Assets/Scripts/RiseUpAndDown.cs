using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RiseUpAndDown : MonoBehaviour
{
    public enum RisingMethod { OnPageOpen, OnPlayerNear, OnlyOnEvent }
    enum RotationAxis { X, Y, Z }
    public enum State { Lowered, Risen, Moving}
    private float riseAmount = 0; //0 - 1
    public float RiseAmount => riseAmount;

    [SerializeField] float timeToRaise = 1;
    [SerializeField] float timeToFall = 1;
    [SerializeField] AnimationCurve standUpMovement;

    [SerializeField] RisingMethod riseMethod = RisingMethod.OnPageOpen;
    public RisingMethod RiseMethod => riseMethod;

    [SerializeField] State currentState = State.Lowered;
    [SerializeField] RotationAxis myRotationAxies = RotationAxis.X;
    public State CurrentState {
        get { return currentState; }
        private set { 
            currentState = value;
            counter = 0;
        }
    }

    [Header("Only for OnPlayerNear")]
    [SerializeField] float distanceFromPlayer = 5;

    [SerializeField] TeleportingObject teleportingObject;
    PlayerMovement player;

    [Header("Events")]
    [SerializeField] UnityEvent OnRise;
    [SerializeField] UnityEvent OnLowered;
    Quaternion myCurrentRotation;
    bool isRising;
    bool oldIsRising;
    float counter;
    bool forced;
    void Start()
    {
        CurrentState = State.Lowered;
        if (!teleportingObject) teleportingObject = GetComponent<TeleportingObject>();
        isRising = false;
        oldIsRising = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!teleportingObject || !teleportingObject.onTopOfBook) {
            forced = false;
            return;
        }
        if(!forced)
            switch (riseMethod) {
                case RisingMethod.OnPageOpen:
                    isRising = true;
                    break;
                case RisingMethod.OnPlayerNear:
                    if (!player || !player.isActiveAndEnabled) {
                        player = FindObjectOfType<PlayerMovement>();
                        transform.localRotation = Quaternion.Euler(GetAxis() * 90);
                        return;
                    }
                    isRising = distanceFromPlayer * distanceFromPlayer >= (transform.position - player.transform.position).sqrMagnitude;
                    break;
                case RisingMethod.OnlyOnEvent:
                    break;
                default:
                    break;
            }
        RisingAndLowering();
    }
    void RisingAndLowering() {

        if ((isRising && CurrentState != State.Risen) || (!isRising && CurrentState != State.Lowered)) {
            if(oldIsRising != isRising) {
                myCurrentRotation = transform.localRotation;
                CurrentState = State.Moving;
            }
            counter += Time.deltaTime;
            float c = counter / (isRising ? timeToRaise : timeToFall);
            c = Mathf.Clamp01(c);
            transform.localRotation = 
                isRising ?
                Quaternion.Euler(GetAxis() * standUpMovement.Evaluate(c) * 90) :
                Quaternion.Lerp(myCurrentRotation, Quaternion.Euler(GetAxis() * 90), c);
            if (c == 1) {
                CurrentState = isRising ? State.Risen : State.Lowered;
                if (isRising) OnRise.Invoke(); else OnLowered.Invoke();
                
            }
        }
        if (CurrentState == State.Lowered) {
            transform.localRotation = Quaternion.Euler(GetAxis() * 90);
            
        }
        oldIsRising = isRising;
    }
    Vector3 GetAxis() {
        switch (myRotationAxies) {
            case RotationAxis.X:
                return Vector3.right;
            case RotationAxis.Y:
                return Vector3.up;
            case RotationAxis.Z:
                return Vector3.forward;
        }
        return Vector3.zero;
    }
    public void RiseUp() {
        isRising = true;
        forced = true;
    }
    public void GoDown() {
        isRising = false;
        forced = true;
    }
    public void DeactivateForced() {
        forced = false;
    }
}
