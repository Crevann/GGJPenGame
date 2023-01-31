using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    [SerializeReference] private EntityData data;
    public string EntityName => data.entityName;
    private EntityHealth health;

    //Logic
    private int damage;
    public int Damage => damage;
    private bool isDead;

    //References
    private Renderer renderer;
    [Header("Fighting references")]
    [SerializeField] private DamagePopup popup;

    private void Awake() {
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
        instPopup.SetText(damage.ToString());
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
