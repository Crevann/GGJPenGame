using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyStatUI : MonoBehaviour
{
    [SerializeField]Image[] spriteEnemys;
    [SerializeField]TextMeshProUGUI[] currentsHealth;
    [SerializeField]Image[] barsHealt;
    [SerializeField] GameObject[] enemyPartUI;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        for (int i = 0; i < enemyPartUI.Length; i++)
        {
            if(GameMGR.Instance.CurrentState != GameState.Fight)
            {
                enemyPartUI[i].SetActive(false);
            }
            else
            {
                if (FightManager.Instance.isBattling)
                {
                    enemyPartUI[i].SetActive(true);
                }
                
            }
        }
       
        for (int i = 0; i < FightManager.Instance.enemies.Length; i++)
        {
            if(!FightManager.Instance.enemies[i] )
            {
                spriteEnemys[i].transform.parent.gameObject.SetActive(false);
                break;
            }
            
            spriteEnemys[i].transform.parent.gameObject.SetActive(true);
            spriteEnemys[i].sprite = FightManager.Instance.enemies[i].Icon;
            currentsHealth[i].text = FightManager.Instance.enemies[i].Health.ToString() + "/" + FightManager.Instance.enemies[i].MaxHealth.ToString();
            barsHealt[i].fillAmount =(float)FightManager.Instance.enemies[i].Health / (float)FightManager.Instance.enemies[i].MaxHealth;
        }

    }
}
