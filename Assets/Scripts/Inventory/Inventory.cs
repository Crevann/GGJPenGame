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
        //Word action
    }

    public Root AddWord(Root root) {
        for (int i = 0; i < maxInventorySize; i++) {
            if (roots[i] == null) {
                roots[i] = root;
                return root;
            }
        }
        return null;
    }

    public Root RemoveWord(int index) {
        if (roots[index]) {
            Root root = roots[index];
            roots[index] = null;
            return root;
        }
        return null;
    }
}
