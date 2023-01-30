using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private int maxHealth;
    private int health;
    public int Health {
        get => health;
        set => health = value >= 0 ? value : 0;
    }
    
    public int SetMaxHealth(int hp) {
        maxHealth = hp;
        health = maxHealth;
        return maxHealth;
    }
}
