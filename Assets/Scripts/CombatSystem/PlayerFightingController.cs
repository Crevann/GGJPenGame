using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerFightingController : MonoBehaviour
{
    [HideInInspector] public RootCombat currentSelectedRoot;
    [HideInInspector] public Entity target;

    private Entity self;

    private void Awake() {
        self = GetComponent<Entity>();
    }
    public void Setup() {
        currentSelectedRoot.Action();
    }

    private void Update() {
        GameMGR.Instance.penHP = self.Health;
    }
    public void Execute() {
        ParticleSystem ps;
        switch (((RootCombatData)currentSelectedRoot.data).avaiableTargets) {
            case Targettables.User:
                self.DealDamage(self.Damage, self);
                self.SpawnEffect(((RootCombatData)currentSelectedRoot.data).effect, self);
                break;
            case Targettables.Single:
                self.DealDamage(self.Damage, target);
                self.SpawnEffect(((RootCombatData)currentSelectedRoot.data).effect, target);
                break;
            case Targettables.Multiple:
                for (int i = 0; i < FightManager.Instance.enemies.Length; i++) {
                    if (FightManager.Instance.enemies[i]) {
                        self.DealDamage(self.Damage, FightManager.Instance.enemies[i]);
                        self.SpawnEffect(((RootCombatData)currentSelectedRoot.data).effect, FightManager.Instance.enemies[i]);
                    }
                }
                break;
            default:
                break;
        }
       
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if(FightManager.Instance.currentTurn == -1) {
            if (GUI.Button(new Rect(200, 0, 150, 20), "Execute")) {
                if (target) {
                    Setup();
                    Execute();
                }
            }
            if (GUI.Button(new Rect(500, 50, 150, 20), "Confirm")) {
                FightManager.Instance.NextPhase();
            }
            for (int i = 0; i < FightManager.Instance.enemies.Length; i++) {
                float y = 30 + 30 * i;
                if (GUI.Button(new Rect(200, y, 150, 20), "Target " + i)) {
                    target = FightManager.Instance.enemies[i];
                }
            }

            if (currentSelectedRoot) {
                GUI.Label(new Rect(400, 0, 300, 20), "Selected: " + currentSelectedRoot.data.root);
            }
            if (target) {
                GUI.Label(new Rect(400, 30, 300, 20), "Target: " + target.EntityName);
            }
        }
    }

#endif
}
