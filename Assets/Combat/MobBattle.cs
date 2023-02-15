using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBattle : MonoBehaviour
{
    [SerializeField] private Entity[] encounter;
    //[SerializeField] private CinemachineVirtualCamera bookCamera;
    public bool defeated;

    [SerializeField] private float battleStartTime;
    private float battleStartTimer;
    private bool battleIsStarting;
    private void OnTriggerEnter(Collider other) {
        PageManager.Instance.OpenToPage(7);
        GameMGR.Instance.mobInfight = this;
        FightManager.Instance.enemiesToSpawn = encounter;
        CameraMgr.Instance.TransitTo(Cameras.FollowFight);
        GameMGR.Instance.CurrentState = GameState.Fight;
        FightManager.Instance.StartFight();
    }

    private void Update() {
        if (defeated) {
            gameObject.SetActive(false);
        }
    }

}