using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Root : MonoBehaviour
{
    [SerializeField] private RootData data;

    public abstract void Action();
}
