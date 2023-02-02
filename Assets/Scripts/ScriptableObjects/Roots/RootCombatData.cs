using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Targettables {
    User,
    Ally,
    Enemy,
    Both
}

[CreateAssetMenu(fileName = "RootCombatData", menuName = "Roots/Combat")]

public class RootCombatData : RootData
{
    public int damage;
    public int stallDamage;
    public Targettables avaiableTargets;
}
