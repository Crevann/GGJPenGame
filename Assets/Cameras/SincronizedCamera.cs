using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SincronizedCamera : MonoBehaviour
{
    [SerializeField] Camera referenceCamera;
    [SerializeField] float positionScaler = 0.01f;
    void LateUpdate()
    {
        transform.rotation = referenceCamera.transform.rotation;
        transform.localPosition = referenceCamera.transform.localPosition * positionScaler;
    }
}
