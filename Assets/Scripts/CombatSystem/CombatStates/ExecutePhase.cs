using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutePhase : StateMachineBehaviour
{
    [SerializeField] private float penActionDelay;
    [SerializeField] private float enemyActionDelay;

    private float penCurrentTimer;
    private float enemyCurrentTimer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Set pen timer
        penCurrentTimer = penActionDelay;
        FightManager.Instance.ExecuteActionPlayer();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Execute Pen and enemy actions with a delay
        penCurrentTimer -= Time.deltaTime;
        if(penCurrentTimer <= 0) {
            enemyCurrentTimer -= Time.deltaTime;
            if(enemyCurrentTimer <= 0) {
                if(FightManager.Instance.currentTurn < FightManager.Instance.enemiesToSpawn.Length) {
                    if (!FightManager.Instance.enemies[FightManager.Instance.currentTurn].IsDead) {
                        FightManager.Instance.enemies[FightManager.Instance.currentTurn].ai.Execute();   
                    }
                    FightManager.Instance.currentTurn++;
                }
                else {
                    animator.SetBool("FightHasEnded", FightManager.Instance.CheckIfAllEnemiesAreDead());
                    FightManager.Instance.NextPhase();
                }
                enemyCurrentTimer = enemyActionDelay;
            }
        }
    }
}
