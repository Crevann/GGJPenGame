using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AttackerAI : BaseEnemyAI {
    //Simplest AI, always attacks
    //Pool is:
    //0 - Hit

    public override Root SelectRoot() {
        currentSelectedRoot = self.RootPool[0];
        return currentSelectedRoot;
    }

    public override Entity SelectTarget() {
        target = FightManager.Instance.pen;
        return target;
    }
}
