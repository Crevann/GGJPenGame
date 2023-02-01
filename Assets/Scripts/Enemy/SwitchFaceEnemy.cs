using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFaceEnemy : MonoBehaviour
{
    [SerializeField] bool isFacingRight;
    [SerializeField] GameObject switchFace;
    [SerializeField] float turningSpeed = 5f;
    Vector3 lastPos;
    void Start()
    {
        isFacingRight = true;
    }

    void Update()
    {
        isFacingRight = lastPos.x < transform.position.x;
        SwitchFace();
        lastPos = transform.position;
    }

    public void SwitchFace()
    {
        switchFace.transform.rotation = Quaternion.Lerp(switchFace.transform.rotation, Quaternion.Euler(new Vector3(0, isFacingRight ? 0 : 180, 0)), Time.deltaTime * turningSpeed);
    }
}
