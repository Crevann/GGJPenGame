using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingFightState : StateMachineBehaviour
{
    private string trigger = "FightInitialized";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        FightManager.Instance.InitializeFight();
        animator.SetTrigger(trigger);
    }
}
