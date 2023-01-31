using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [HideInInspector] public Mob[] mobs;
    [SerializeField] public BoxCollider collider;
    void Start()
    {
        List<Transform> transforms = new List<Transform>();
        mobs = transform.Find("Mobs").GetComponentsInChildren<Mob>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
