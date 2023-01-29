using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private int maxInventorySize = 4;
    public Root[] roots;

    private void Awake() {
        roots = new Root[maxInventorySize];
    }

    public void UseWord(int index) {
        roots[index].Action();
        RemoveRoot(index);
    }

    public Root AddRoot(Root root) {
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
    private void OnGUI() {
        GUI.Label(new Rect(0, 0, 200, 30), "Inventory:");
        for (int i = 0; i < roots.Length; i++) {
            if (roots[i]) {
                float y = 35 + (35 * i);
                GUI.Label(new Rect(0, y, 150, 30), roots[i].data.name);
                if (GUI.Button(new Rect(160, y, 50, 20), "Use")) {
                    UseWord(i);
                }
            }
        } 
    }
#endif
}
