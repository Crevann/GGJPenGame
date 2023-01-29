using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private int maxInventorySize = 4;
    public Word[] words;

    private void Awake() {
        words = new Word[maxInventorySize];
    }

    public void UseWord(int index) {
        //Word action
    }

    public Word AddWord(Word word) {
        for (int i = 0; i < maxInventorySize; i++) {
            if (words[i] == null) {
                words[i] = word;
                return word;
            }
        }
        return null;
    }

    public Word RemoveWord(int index) {
        if (words[index]) {
            Word word = words[index];
            words[index] = null;
            return word;
        }
        return null;
    }
}
