using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBattle : MonoBehaviour
{
    [SerializeField] private Entity[] encounter;
    private void OnTriggerEnter(Collider other) {
        GameMGR.Instance.mobInfight = this;
        FightManager.Instance.enemiesToSpawn = encounter;
        GameMGR.Instance.CurrentState = GameState.Fight;
        LevelMgr.Instance.OpenToPage(7);
        FightManager.Instance.StartFight();
    }
}
