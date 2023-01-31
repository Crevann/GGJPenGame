using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private int maxHealth;
    private int health;
    public int Health {
        get => health;
        set {
            if (value < 0) {
                health = 0;
            }else if(value > maxHealth) {
                health = maxHealth;
            }
            else {
                health = value;
            }
        }
    }
    
    public int SetMaxHealth(int hp) {
        maxHealth = hp;
        health = maxHealth;
        return maxHealth;
    }
}
