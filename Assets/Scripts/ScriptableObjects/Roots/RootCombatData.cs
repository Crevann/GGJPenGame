using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Targettables {
    User,
    Single,
    Multiple
}

public enum StatusEffects {
    Buff,
    Debuff,
    Stun,
    Counter
}
[CreateAssetMenu(fileName = "RootCombatData", menuName = "Roots/Combat")]

public class RootCombatData : RootData
{
    public int damage;
    public int stallDamage;
    public Targettables avaiableTargets;
    public string description;
    public ParticleSystem effect;
}
