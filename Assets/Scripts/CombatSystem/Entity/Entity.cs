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

    private void Awake() {
        health = GetComponent<EntityHealth>();
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
    }

    public bool CheckDead() {
        if(health.Health <= 0) {
            isDead = true;
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}
