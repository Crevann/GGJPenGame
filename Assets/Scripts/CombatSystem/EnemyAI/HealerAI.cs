using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAI : BaseEnemyAI {
    //Simplest AI, always attacks
    //Pool is a random move of heals

    public override Root SelectRoot() {
        currentSelectedRoot = self.RootPool[Random.Range(0, self.RootPool.Length)];
        return currentSelectedRoot;
    }

    public override Entity SelectTarget() {
        Entity lowestHealthEntity = null;
        float lowestHealth = 1;
        foreach (var entity in FightManager.Instance.enemies) {
            if (entity && !entity.IsDead) {
                //Get normalized entity hp
                float entityHPNormalized = (float)entity.Health / (float)entity.MaxHealth;
                Debug.Log("Entity normalized HP: " + entityHPNormalized.ToString());
                if (!lowestHealthEntity) {
                    lowestHealthEntity = self;
                    lowestHealth = (float)self.Health / (float)self.MaxHealth;
                }
                if (entityHPNormalized < lowestHealth) {
                    lowestHealthEntity = entity;
                }
            }
        }
        target = lowestHealthEntity;
        return target;
    }
}
