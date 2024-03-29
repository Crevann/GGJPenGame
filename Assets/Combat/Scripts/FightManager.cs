using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using Unity.Mathematics;

//!!Lasciate ogni speranza voi ch'entrate!!
public class FightManager : Singleton<FightManager>
{
    [SerializeField] private int maxEnemies = 4;

    public Entity pen;
    public Entity[] enemies;
    public Entity[] enemiesToSpawn;
    [HideInInspector] public int currentTurn; //If -1 is player turn
    public bool isBattling;
    private bool penIsDead;

    //Camera management
    [SerializeField] CinemachineVirtualCamera fightingCam;
    [SerializeField] private float returnToNormalSpeed;
    private CinemachineBasicMultiChannelPerlin camNoise;

    //References
    private Animator FSM;
    private PlayerFightingController playerFightingController;
    public PlayerFightingController PlayerFightingController => playerFightingController;
    [SerializeField] private Transform penPosition;
    [SerializeField] private Transform[] enemyPositions;
    [SerializeField] private BackgroundPeople stadium;

    //FSM triggers
    private string startFight = "StartFight";
    private string nextState = "NextState";
    private string fightInitialized = "FightInitialized";
    private string fightHasEnded = "FightHasEnded";
    private string engageStall = "EngageStall";

    private void Start() {
        enemies = new Entity[maxEnemies];
        //enemiesToSpawn = new Entity[maxEnemies];
    }
    private void Awake() {
        FSM = GetComponent<Animator>();
        playerFightingController = pen.GetComponent<PlayerFightingController>();
        camNoise = fightingCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void SetPenHealth() {
        pen.HealthComponent.SetMaxHealth(GameMGR.Instance.penMaxHP);
        pen.HealthComponent.Health = GameMGR.Instance.penHP;
    }
    
    public void InitializeFight() {
        stadium.gameObject.SetActive(true);
        stadium.OpenStadium();
        pen.gameObject.SetActive(true);
        SetPenHealth();
        pen.transform.position = penPosition.position;
        for (int i = 0; i < enemiesToSpawn.Length; i++){
            Entity spawnedEnemy = Instantiate(enemiesToSpawn[i], enemyPositions[i]);
            spawnedEnemy.Initialize();
            //spawnedEnemy.transform.localPosition = new Vector3(0, spawnedEnemy.GetEntitySize().y * 0.5f, 0);
            enemies[i] = spawnedEnemy;
        }
        currentTurn = -1;
        fightingCam.Priority = 5;
        isBattling = true;
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

    public void NextPhase() {
        FSM.SetTrigger(nextState);
    }
    public void ExecuteActionPlayer() {
        playerFightingController.Setup();
        playerFightingController.Execute();
    }

    public bool CheckIfAllEnemiesAreDead() {
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i]) {
                if (!enemies[i].IsDead) {
                    return false;
                }
            }
        }
        return true;
    }

    public bool CheckIfPenIsDead() {
        return pen.CheckDead();
    }
    
    public void EnemyDecisions() {
        foreach (Entity enemy in enemies) {
            if (enemy) {
                enemy.ai.SelectRoot();
                enemy.ai.SelectTarget();
                enemy.ai.UseRoot();
            }
        }
    }

    public void MakeEnemyAttack(int enemy) {
        for (int i = 0; i < enemyPositions.Length; i++) {
            if(i == enemy) {
                enemyPositions[i].GetComponent<MovePosition>().movementState = MovementState.TOATTACK;
            }
            else {
                enemyPositions[i].GetComponent<MovePosition>().movementState = MovementState.TOBACK;
            }
        }
    }

    public void ResetEnemyPositions() {
        for (int i = 0; i < enemyPositions.Length; i++) {
            enemyPositions[i].GetComponent<MovePosition>().movementState = MovementState.TOORIGINAL;
        }
    }

    public bool CheckTargetWords() {
        if(playerFightingController.currentSelectedRoot.data.root == playerFightingController.target.ai.CurrentSelectedRoot.data.root) {
            return true;
        }
        return false;
    }

    public void CloseStadium() {
        stadium.CloseStadium();
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if(GUI.Button(new Rect(500, 0, 100, 30), "Execute AI turn")) {
            foreach(Entity enemy in enemies) {
                if (enemy) {
                    enemy.ai.SelectRoot();
                    enemy.ai.SelectTarget();
                    enemy.ai.UseRoot();
                    enemy.ai.Execute();
                }
            }
        }
    }
#endif
}
