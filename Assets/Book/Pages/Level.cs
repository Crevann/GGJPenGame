using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [HideInInspector] public TeleportingObject[] teleportingObjects;
    [SerializeField] public BoxCollider collider;
    void Start()
    {
        List<Transform> transforms = new List<Transform>();
        teleportingObjects = transform.GetComponentsInChildren<TeleportingObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
