using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWordsPhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //This will enable the UI
        /*if (FightManager.Instance.CheckTargetWords()) {
            Debug.Log("Words are the same, engage stall");
            //animator.SetTrigger("EngageStall");
            FightManager.Instance.NextPhase();
        }
        else {
            FightManager.Instance.NextPhase();
        }*/
    }
}
