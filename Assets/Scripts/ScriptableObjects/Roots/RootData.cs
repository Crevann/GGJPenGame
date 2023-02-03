using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RootWorldData", menuName = "Roots/World")]
public class RootData : ScriptableObject
{
    public string root;
    public GameState context = GameState.World;
    public string[] suffixes;
}
