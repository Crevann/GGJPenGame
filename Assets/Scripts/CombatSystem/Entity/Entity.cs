using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    [SerializeReference] private EntityData data;
    [Header("Sprites")]
    [SerializeField] private Sprite idle;
    public Sprite Icon => idle;
    [SerializeField] private Sprite attack;
    public string EntityName => data.entityName;
    public RootCombat[] RootPool => ((EnemyData)data).rootPool;
    private EntityHealth health;
    public EntityHealth HealthComponent => health;
    public int Health => health.Health;
    public int MaxHealth => health.MaxHealth;
    public bool EntityHasMaxedHp => health.MaxedHealth;

    //Status effect
    public bool stunned;
    public bool counter;
    public int weakness;
    public int multiplier;

    //Logic
    private int damage;
    public int Damage => damage;
    private bool isDead;
    public bool IsDead => isDead;

    //References
    private SpriteRenderer renderer;
    public BaseEnemyAI ai; //Get only if it has AI
    [Header("Fighting references")]
    [SerializeField] private DamagePopup popup;

    private void Awake() {
        if(!TryGetComponent<BaseEnemyAI>(out ai)) {
            Debug.Log("Entity is not AI");
        }
        health = GetComponent<EntityHealth>();
        renderer = GetComponent<SpriteRenderer>();
        multiplier = 1;
    }
    public void Initialize() {
        health.SetMaxHealth(data.health);
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }
    public void DealDamage(int damage, Entity target, bool stun, int weakness, bool showPopup, int multiplier, bool counter) {
        //Do some cool camera shake or something
        int realDamage = (damage * this.multiplier) + target.weakness;
        if (target.counter && !counter && damage > 0) {
            DealDamage(damage, this, stun, weakness, showPopup, multiplier, counter);
            target.counter = false;
            DamagePopup instPopup = Instantiate<DamagePopup>(popup);
            instPopup.transform.position = target.transform.position + Vector3.forward * -1.1f;
            instPopup.SetText("COUNTER");
            instPopup.text.fontSize = 14;
            DamagePopup instPopupNumber = Instantiate<DamagePopup>(popup);
            instPopupNumber.transform.position = target.transform.position + Vector3.up * 1.3f + Vector3.forward * -1;
            instPopupNumber.SetText(Mathf.Abs(realDamage).ToString());
            instPopupNumber.text.fontSize = 16;
            return;
        }
        target.health.Health -= realDamage;
        Debug.Log(target.health.Health);
        if (target.CheckDead()) {
            Debug.Log("Is dead");
        }
        if (showPopup) {
            DamagePopup instPopup = Instantiate<DamagePopup>(popup);
            instPopup.transform.position = target.transform.position + Vector3.forward * -1;
            instPopup.SetText(Mathf.Abs(realDamage).ToString());
        }
        FightManager.Instance.ShakeCamera(realDamage);
        target.stunned = stun;
        target.weakness = weakness;
        target.counter = counter;
        this.multiplier = multiplier;
    }

    public void SpawnEffect(ParticleSystem effect, Entity target) {
        ParticleSystem ps = Instantiate<ParticleSystem>(effect);
        ps.transform.position += target.transform.position;
    }

    public bool CheckDead() {
        if(health.Health <= 0) {
            isDead = true;
            gameObject.SetActive(false); //Temp
            return true;
        }
        return false;
    }

    public void SwitchSprites() {
        renderer.sprite = renderer.sprite == idle ? attack : idle;
    }

    public Vector3 GetEntitySize() {
        return renderer.bounds.size;
    }

    //ONLY FOR PLAYER ANIMATION
    public void AttackPlayer() {
        FightManager.Instance.ExecuteActionPlayer();
    }
}
