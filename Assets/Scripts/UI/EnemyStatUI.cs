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
            currentsHealth[i].text = FightManager.Instance.enemies[i].Health.ToString() + "/" + FightManager.Instance.enemies[i].MaxHealth.ToString();
            barsHealt[i].fillAmount =(float)FightManager.Instance.enemies[i].MaxHealth / (float)FightManager.Instance.enemies[i].Health;
        }
    }
}
