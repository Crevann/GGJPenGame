using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

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
    public EventReference SFX;
    public bool showPopup;

    [Header("Status effect")]
    public bool stun;
    public bool counter;
    public int multiplier = 1;
    public int weakness;

}



