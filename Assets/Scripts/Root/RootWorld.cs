using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RootWprldData", menuName = "Roots/World")]
abstract public class RootWorld : Root
{
    [SerializeField] protected GameObject affectedGO;
}
