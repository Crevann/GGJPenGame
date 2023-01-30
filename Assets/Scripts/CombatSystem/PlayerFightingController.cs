using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerFightingController : MonoBehaviour
{
    public RootCombat currentSelectedRoot;
    public Entity target;

    private Entity self;

    private void Awake() {
        self = GetComponent<Entity>();
    }
    public void Setup() {
        currentSelectedRoot.Action();
    }

    public void Execute() {
        self.DealDamage(self.Damage, target);
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if (GUI.Button(new Rect(200, 0, 150, 20), "Execute")) {
            if (target) {
                Setup();
                Execute();
            }
        }
        for (int i = 0; i < FightManager.Instance.enemies.Length; i++) {
            float y = 30 + 30 * i;
            if(GUI.Button(new Rect(200, y, 150, 20), "Target " + i)){
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

#endif
}
