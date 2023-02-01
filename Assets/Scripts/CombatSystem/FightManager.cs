using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using Unity.Mathematics;

public class FightManager : Singleton<FightManager>
{
    [SerializeField] private int maxEnemies = 4;

    public Entity pen;
    public Entity[] enemies;
    public Entity[] enemiesToSpawn;
    [HideInInspector] public int currentTurn; //If -1 is player turn

    //Camera management
    [SerializeField] CinemachineVirtualCamera fightingCam;
    [SerializeField] private float returnToNormalSpeed;
    private CinemachineBasicMultiChannelPerlin camNoise;

    //References
    private Animator FSM;
    [SerializeField] private Transform[] enemyPositions;

    //FSM triggers
    private string startFight = "StartFight";

    private void Start() {
        enemies = new Entity[maxEnemies];
        //enemiesToSpawn = new Entity[maxEnemies];
    }
    private void Awake() {
        FSM = GetComponent<Animator>();
        camNoise = fightingCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void InitializeFight() {
        pen.Initialize(); //Just for testing purposes, move this to start of the game
        for(int i = 0; i < enemiesToSpawn.Length; i++){
            Entity spawnedEnemy = Instantiate(enemiesToSpawn[i], enemyPositions[i]);
            spawnedEnemy.Initialize();
            spawnedEnemy.transform.localPosition = new Vector3(0, spawnedEnemy.GetEntitySize().y * 0.5f, 0);
            enemies[i] = spawnedEnemy;
        }
        currentTurn = -1;
        fightingCam.Priority = 5;
    }

    private void Update() {
        camNoise.m_AmplitudeGain = Mathf.Lerp(camNoise.m_AmplitudeGain, 0, Time.deltaTime * returnToNormalSpeed);
    }

    public void ShakeCamera(float strength) {
        camNoise.m_AmplitudeGain = strength;
    }

    public void StartFight() {
        FSM.SetTrigger(startFight);
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if(GUI.Button(new Rect(400, 0, 100, 30), "Execute AI turn")) {
            foreach(Entity enemy in enemies) {
                enemy.ai.SelectRoot();
                enemy.ai.SelectTarget();
                enemy.ai.UseRoot();
                enemy.ai.Execute();
            }
        }
    }
#endif
}
