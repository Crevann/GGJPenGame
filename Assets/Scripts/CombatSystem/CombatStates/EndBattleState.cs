using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EndBattleState : StateMachineBehaviour
{
    private string trigger = "FightInitialized";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        LevelMgr.Instance.OpenToPage(GameMGR.Instance.currentLevel);
        CameraMgr.Instance.ChooseCamera(Cameras.BookView);
        GameMGR.Instance.CurrentState = GameState.World;
    }
}
