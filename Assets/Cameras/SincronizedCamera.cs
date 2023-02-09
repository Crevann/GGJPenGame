using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SincronizedCamera : MonoBehaviour
{
    [SerializeField] Camera referenceCamera;
    [SerializeField] float positionScaler = 0.01f;
    [SerializeField] Vector3 reference = Vector3.one;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = referenceCamera.transform.rotation;
        reference = referenceCamera.transform.localPosition;
        transform.localPosition = referenceCamera.transform.localPosition * positionScaler;
    }
    void FixedUpdate()
    {
        transform.rotation = referenceCamera.transform.rotation;
        reference = referenceCamera.transform.localPosition;
        transform.localPosition = referenceCamera.transform.localPosition * positionScaler;
    }
    void LateUpdate()
    {
        transform.rotation = referenceCamera.transform.rotation;
        reference = referenceCamera.transform.localPosition;
        transform.localPosition = referenceCamera.transform.localPosition * positionScaler;
    }
}
