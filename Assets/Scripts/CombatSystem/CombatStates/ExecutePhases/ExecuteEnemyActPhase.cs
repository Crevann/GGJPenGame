using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteEnemyActPhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!FightManager.Instance.enemies[FightManager.Instance.currentTurn].stunned) {
            FightManager.Instance.enemies[FightManager.Instance.currentTurn].SwitchSprites();
            FightManager.Instance.enemies[FightManager.Instance.currentTurn].ai.Execute();
        }
        

        if (FightManager.Instance.CheckIfPenIsDead()) {
            animator.SetBool("FightHasEnded", true);
            animator.SetBool("AllEnemiesActed", true);
            return;
        }
    }
}
