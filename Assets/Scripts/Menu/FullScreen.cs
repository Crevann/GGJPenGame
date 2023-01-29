using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class FullScreen : MonoBehaviour
{
    [SerializeField]Camera camera;
    [SerializeField] LayerMask collider;

    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, collider))
        {

        }
    }
}
