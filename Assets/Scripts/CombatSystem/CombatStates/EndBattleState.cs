using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EndBattleState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        LevelMgr.Instance.OpenToPage(GameMGR.Instance.currentLevel);
        GameMGR.Instance.mobInfight.defeated = true;
        GameMGR.Instance.CurrentState = GameState.World;
        CameraMgr.Instance.ChooseCamera(Cameras.FollowPlayer);
        if (FightManager.Instance.pen.IsDead) {
            GameMGR.Instance.penIsDead = true;
        }
        FightManager.Instance.isBattling = false;
    }
}
