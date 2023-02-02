using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteMovePhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        FightManager.Instance.MakeEnemyAttack(FightManager.Instance.currentTurn);
    }
}
