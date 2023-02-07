using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState {
    NONE,
    TOATTACK,
    TOBACK,
    TOORIGINAL
}
public class MovePosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 backPosition;
    public MovementState movementState;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackOffsetZ;
    [SerializeField] private float moveSpeed;
    void Awake()
    {
        originalPosition = transform.position;
        backPosition = transform.position - new Vector3(0, 0, attackOffsetZ);
    }

    // Update is called once per frame
    void Update()
    {
        switch (movementState) {
            case MovementState.NONE:
                break;
            case MovementState.TOATTACK:
                transform.position = Vector3.Lerp(transform.position, attackPosition.position, Time.deltaTime * moveSpeed);
                break;
            case MovementState.TOBACK:
                transform.position = Vector3.Lerp(transform.position, backPosition, Time.deltaTime * moveSpeed);
                break;
            case MovementState.TOORIGINAL:
                transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * moveSpeed);
                break;
            default:
                break;
        }
    }
}
