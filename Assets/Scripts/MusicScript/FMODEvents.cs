using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field : Header("changePage SFX")]
    [field : SerializeField] public EventReference changePage { get; private set; }

    [field: Header("PenMovement SFX")]
    [field: SerializeField] public EventReference penMovement { get; private set; }
    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("FMODEvents already exist in the scene");

        }
        instance = this;
    }
}
