using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteEnemyActPhase : StateMachineBehaviour
{
    [SerializeField] private DamagePopup popup;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!FightManager.Instance.enemies[FightManager.Instance.currentTurn].stunned) {
            FightManager.Instance.enemies[FightManager.Instance.currentTurn].SwitchSprites();
            FightManager.Instance.enemies[FightManager.Instance.currentTurn].ai.Execute();
            DamagePopup instPopup = Instantiate<DamagePopup>(popup);
            instPopup.transform.position = FightManager.Instance.enemies[FightManager.Instance.currentTurn].transform.position + Vector3.forward * -1.2f + Vector3.up * -1.2f;
            instPopup.SetText(FightManager.Instance.enemies[FightManager.Instance.currentTurn].ai.CurrentSelectedRoot.data.root);
            instPopup.text.fontSize = 14;
        }
        else {
            DamagePopup instPopup = Instantiate<DamagePopup>(popup);
            instPopup.transform.position = FightManager.Instance.enemies[FightManager.Instance.currentTurn].transform.position + Vector3.forward * -1.2f;
            instPopup.SetText("Stunned");
            instPopup.text.fontSize = 14;
        }

        if (FightManager.Instance.enemies[FightManager.Instance.currentTurn].counter) {
            FightManager.Instance.enemies[FightManager.Instance.currentTurn].counter = false;
        }

        if (FightManager.Instance.CheckIfPenIsDead()) {
            animator.SetBool("FightHasEnded", true);
            animator.SetBool("AllEnemiesActed", true);
            return;
        }
    }
}
