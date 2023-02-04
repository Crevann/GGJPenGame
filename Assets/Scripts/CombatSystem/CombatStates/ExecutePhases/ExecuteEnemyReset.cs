using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteEnemyReset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(!FightManager.Instance.enemies[FightManager.Instance.currentTurn].stunned) {
            FightManager.Instance.enemies[FightManager.Instance.currentTurn].SwitchSprites();
        }
        FightManager.Instance.enemies[FightManager.Instance.currentTurn].stunned = false;
        FightManager.Instance.ResetEnemyPositions();
        FightManager.Instance.currentTurn++;
        if(FightManager.Instance.currentTurn >= FightManager.Instance.enemiesToSpawn.Length) {
            animator.SetBool("AllEnemiesActed", true);
        }

        //Check if pen is dead
        if (FightManager.Instance.pen.IsDead) {
            animator.SetBool("FightHasEnded", true);
        }
    }
}
