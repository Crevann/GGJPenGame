using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutePenPhase : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool("AllEnemiesActed", false);
        Animation ani = FightManager.Instance.pen.GetComponent<Animation>();
        ani.Play();        
    }
}
