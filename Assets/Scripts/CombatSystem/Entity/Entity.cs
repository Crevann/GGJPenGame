using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    [SerializeReference] private EntityData data;
    public string EntityName => data.entityName;
    public RootCombat[] RootPool => ((EnemyData)data).rootPool;
    private EntityHealth health;
    public int Health => health.Health;
    public bool EntityHasMaxedHp => health.MaxedHealth;

    //Logic
    private int damage;
    public int Damage => damage;
    private bool isDead;
    public bool IsDead => IsDead;

    //References
    private Renderer renderer;
    public BaseEnemyAI ai; //Get only if it has AI
    [Header("Fighting references")]
    [SerializeField] private DamagePopup popup;

    private void Awake() {
        if(!TryGetComponent<BaseEnemyAI>(out ai)) {
            Debug.Log("Entity is not AI");
        }
        health = GetComponent<EntityHealth>();
        renderer = GetComponent<Renderer>();
    }
    public void Initialize() {
        health.SetMaxHealth(data.health);
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }
    public void DealDamage(int damage, Entity target) {
        //Do some cool camera shake or something
        target.health.Health -= damage;
        Debug.Log(target.health.Health);
        if (target.CheckDead()) {
            Debug.Log("Is dead");
        }
        DamagePopup instPopup = Instantiate<DamagePopup>(popup);
        instPopup.transform.position = target.transform.position;
        instPopup.SetText(Mathf.Abs(damage).ToString());
        FightManager.Instance.ShakeCamera(damage);
    }

    public bool CheckDead() {
        if(health.Health <= 0) {
            isDead = true;
            gameObject.SetActive(false); //Temp
            return true;
        }
        return false;
    }

    public Vector3 GetEntitySize() {
        return renderer.bounds.size;
    }
}
