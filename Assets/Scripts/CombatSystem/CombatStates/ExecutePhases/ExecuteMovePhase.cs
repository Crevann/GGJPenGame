using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteMovePhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Check if enemies needs to be skipped
        for(int i = FightManager.Instance.currentTurn; i < FightManager.Instance.enemiesToSpawn.Length; i++) {
            if (!FightManager.Instance.enemies[i].IsDead) {
                break;
            }
            FightManager.Instance.currentTurn++;
        }

        //Check if all enemies are dead
        if (FightManager.Instance.CheckIfAllEnemiesAreDead()) {
            animator.SetBool("FightHasEnded", true);
            animator.SetBool("AllEnemiesActed", true);
            return;
        }

        FightManager.Instance.MakeEnemyAttack(FightManager.Instance.currentTurn);
    }
}
