using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class HealerAI : BaseEnemyAI {
    //Simplest AI, always attacks
    //Pool is:
    //0 - Hit
    //1 - Heal

    public override Root SelectRoot() {
        currentSelectedRoot = self.RootPool[1];
        return currentSelectedRoot;
    }

    public override Entity SelectTarget() {
        Entity lowestHealthEntity = self;
        foreach (var entity in FightManager.Instance.enemies) {
            if (entity) {
                if (entity.Health < lowestHealthEntity.Health && !entity.EntityHasMaxedHp) {
                    lowestHealthEntity = entity;
                }
            }
        }
        target = lowestHealthEntity;
        return target;
    }
}
