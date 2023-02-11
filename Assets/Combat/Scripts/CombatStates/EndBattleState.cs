using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        FightManager.Instance.pen.HealthComponent.Health = FightManager.Instance.pen.MaxHealth;
        GameMGR.Instance.penHP = GameMGR.Instance.penMaxHP;
        FightManager.Instance.CloseStadium();
        PageManager.Instance.OpenToPage(GameMGR.Instance.currentLevel);
        GameMGR.Instance.mobInfight.defeated = true;
        GameMGR.Instance.CurrentState = GameState.World;
        CameraMgr.Instance.TransitTo(Cameras.FollowPlayer);
        if (FightManager.Instance.pen.IsDead) {
            GameMGR.Instance.penIsDead = true;
        }
        FightManager.Instance.isBattling = false;
    }
}
