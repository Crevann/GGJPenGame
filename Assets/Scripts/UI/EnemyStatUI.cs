using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyStatUI : MonoBehaviour
{
    Image[] spriteEnemys;
    TextMeshProUGUI[] currentsHealth;
    Image[] barsHealt;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        for (int i = 0; i < FightManager.Instance.enemies.Length; i++)
        {
            //spriteEnemys[i].sprite = 
        }
    }
}
