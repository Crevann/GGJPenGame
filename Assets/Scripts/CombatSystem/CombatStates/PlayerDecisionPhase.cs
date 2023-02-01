using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecisionPhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //This will enable the UI
        FightManager.Instance.currentTurn = -1;
    }
}
