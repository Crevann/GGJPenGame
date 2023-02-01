using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionPhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Enemies will all decide 
        FightManager.Instance.currentTurn = 0;
        FightManager.Instance.EnemyDecisions();
        FightManager.Instance.NextPhase();
    }
}
