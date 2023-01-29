using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Avaiability {
    World,
    Combat
}
public class RootData : ScriptableObject
{
    public string root;
    public Avaiability context = Avaiability.World;
    public string[] suffixes;
}
