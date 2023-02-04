using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AttackerAI : BaseEnemyAI {
    //Simplest AI, always attacks
    //Random pool of 4 random moves:
    //0 - Hit

    public override Root SelectRoot() {
        currentSelectedRoot = self.RootPool[Random.Range(0, self.RootPool.Length)];
        return currentSelectedRoot;
    }

    public override Entity SelectTarget() {
        target = FightManager.Instance.pen;
        return target;
    }
}
