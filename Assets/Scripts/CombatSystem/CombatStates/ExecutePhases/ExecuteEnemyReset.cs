using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteEnemyReset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        FightManager.Instance.enemies[FightManager.Instance.currentTurn].SwitchSprites();
        FightManager.Instance.ResetEnemyPositions();
        FightManager.Instance.currentTurn++;
        if(FightManager.Instance.currentTurn >= FightManager.Instance.enemiesToSpawn.Length) {
            animator.SetBool("AllEnemiesActed", true);
        }

        //Check if pen is dead
    }
}
