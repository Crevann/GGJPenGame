using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHitTest : RootCombat {
    public override void Action() {
        RootCombatData combatData = (RootCombatData)data;
        user.SetDamage(combatData.damage);
    }
}
