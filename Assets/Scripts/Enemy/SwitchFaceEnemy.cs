using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFaceEnemy : MonoBehaviour
{
    [SerializeField] bool isFacingRight;
    [SerializeField] GameObject switchFace;
    [SerializeField] float turningSpeed = 5f;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPos.x < transform.position.x && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (lastPos.x > transform.position.x && isFacingRight)
        {
            isFacingRight = false;
        }
        SwitchFace();
        lastPos = transform.position;
    }

    public void SwitchFace()
    {
        if (isFacingRight)
        {
            switchFace.transform.rotation = Quaternion.Lerp(switchFace.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.deltaTime * turningSpeed);
        }
        else
        {
            switchFace.transform.rotation = Quaternion.Lerp(switchFace.transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), Time.deltaTime * turningSpeed);
        }
    }
}
