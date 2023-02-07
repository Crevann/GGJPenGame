using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteMovePhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Check if enemies needs to be skipped
        for(int i = FightManager.Instance.currentTurn; i < FightManager.Instance.enemiesToSpawn.Length; i++) {
            Debug.Log(i);
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

        if (FightManager.Instance.currentTurn >= FightManager.Instance.enemiesToSpawn.Length) {
            animator.SetBool("AllEnemiesActed", true);
            FightManager.Instance.NextPhase();
            return;
        }

        FightManager.Instance.MakeEnemyAttack(FightManager.Instance.currentTurn);
    }
}
