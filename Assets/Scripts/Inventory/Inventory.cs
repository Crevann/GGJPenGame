using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private int maxInventorySize = 4;
    public Root[] roots;

    private Entity user;
    private PlayerFightingController controller;

    private void Awake() {
        roots = new Root[maxInventorySize];
        user = FightManager.Instance.pen;
        controller = FightManager.Instance.pen.GetComponent<PlayerFightingController>();
    }

    public Root UseWord(int index) {
        roots[index].Action();
        return roots[index];
    }

    public Root SelectRoot(int index) {
        return roots[index];
    }

    public Root AddRoot(Root root) {
        if(root is RootCombat) {
            ((RootCombat)root).SetUser(user);
        }
        for (int i = 0; i < maxInventorySize; i++) {
            if (roots[i] == null) {
                roots[i] = root;
                return root;
            }
        }
        return null;
    }

    public Root RemoveRoot(int index) {
        if (roots[index]) {
            Root root = roots[index];
            roots[index] = null;
            return root;
        }
        return null;
    }

#if UNITY_EDITOR
    [SerializeField] private Root rootToAdd;
    private void OnGUI() {
        GUI.Label(new Rect(0, 0, 200, 30), "Inventory:");
        for (int i = 0; i < roots.Length; i++) {
            if (roots[i]) {
                float y = 35 + (35 * i);
                GUI.Label(new Rect(0, y, 150, 30), roots[i].data.name);
                if (GUI.Button(new Rect(100, y, 50, 20), "Select")) {
                    controller.currentSelectedRoot = (RootCombat)SelectRoot(i);
                }
            }
        }
        if (GUI.Button(new Rect(100, 200, 50, 20), "Add")) {
            AddRoot(rootToAdd);
        }
    }
#endif
}
