using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [HideInInspector] public TeleportingObject[] mobs;
    [SerializeField] public BoxCollider collider;
    void Start()
    {
        List<Transform> transforms = new List<Transform>();
        mobs = transform.Find("Mobs").GetComponentsInChildren<TeleportingObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
