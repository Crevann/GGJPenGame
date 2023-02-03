using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum GameState { Menu, World, Fight, CutScene}
public class GameMGR : Singleton<GameMGR>
{
    [SerializeField] private EntityData penData;
    public int penMaxHP;
    public int penHP;

    [System.Serializable]
    public class KeyValuePair {
        public GameState key;
        public List<GameObject> val;
    }
    [SerializeField] List<KeyValuePair> MyList = new List<KeyValuePair>();
    Dictionary<GameState, List<GameObject>> dictionary = new Dictionary<GameState, List<GameObject>>();

    void Awake() {
        foreach (var kvp in MyList) {
            dictionary[kvp.key] = kvp.val;
        }
    }
    private void Start() {
        CurrentState = GameState.Menu;
        InitializePen();
    }

    private GameState currentState = GameState.Menu;
    public GameState CurrentState {
        get => currentState;
        set {
            currentState = value;
            ChangeState();
            AudioManager.instance.SetMusicArea(currentState);
            Debug.Log(currentState);
        }
    }
    private void ChangeState() {
        for (int i = 0; i < dictionary.Count; i++) {
            if ((GameState)i == currentState) 
                foreach (GameObject item in dictionary[(GameState)i]) {
                    item.SetActive(true);
                }
            else
                foreach (GameObject item in dictionary[(GameState)i]) {
                    item.SetActive(false);
                }
        }
    }
    public void ChangeState(int state) { 
        CurrentState = (GameState)state;
    }

    private void InitializePen() {
        penMaxHP = penData.health;
        penHP = penData.health;
    }
}
