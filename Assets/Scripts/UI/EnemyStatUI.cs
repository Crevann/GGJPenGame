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
            if(!FightManager.Instance.enemies[i])
            {
                spriteEnemys[i].gameObject.SetActive(false);
                break;
            }
            spriteEnemys[i].sprite = FightManager.Instance.enemies[i].Icon;
        }
    }
}
