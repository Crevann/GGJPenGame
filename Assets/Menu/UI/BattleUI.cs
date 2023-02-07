using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rootDescription;
    [SerializeField] TextMeshProUGUI rootName;
    [SerializeField] Button[] buttons;
    [SerializeField]float speed;
    [SerializeField] RectTransform target;
    RectTransform myrect;
    Vector3 oldRect;
    bool isOpened = false;
    void Start()
    {
        myrect = GetComponent<RectTransform>();
        oldRect = myrect.localPosition;
    }

    
    void Update()
    {
        
        if(FightManager.Instance.currentTurn == -1)
        {
            
            myrect.localPosition = new Vector3(myrect.localPosition.x,Mathf.Lerp(myrect.localPosition.y, target.localPosition.y, speed * Time.deltaTime), myrect.localPosition.z);
            for (int i = 0; i < FightManager.Instance.enemies.Length; i++)
            {
                if (!FightManager.Instance.enemies[i])
                {
                    buttons[i].gameObject.SetActive(false);
                    continue;
                }
                else
                {
                    buttons[i].gameObject.SetActive(true);
                }
                if (!FightManager.Instance.PlayerFightingController.currentSelectedRoot)
                {
                    buttons[i].interactable = false;
                }
                else
                {
                    buttons[i].interactable = true;
                }
                if (FightManager.Instance.enemies[i].IsDead)
                {
                    buttons[i].interactable = false;
                }
                
                buttons[i].GetComponent<Image>().sprite = FightManager.Instance.enemies[i].Icon;
            }
            if (FightManager.Instance.PlayerFightingController.currentSelectedRoot)
            {
                rootDescription.text = ((RootCombatData)FightManager.Instance.PlayerFightingController.currentSelectedRoot.data).description;
                rootName.text = ((RootCombatData)FightManager.Instance.PlayerFightingController.currentSelectedRoot.data).root;
            }       
        }
        else
        {
            myrect.localPosition = new Vector3(myrect.localPosition.x, Mathf.Lerp(myrect.localPosition.y, oldRect.y, speed * Time.deltaTime),  myrect.localPosition.z);
        }
    }


    public void SelectEnemy(int index)
    {
        FightManager.Instance.PlayerFightingController.target = FightManager.Instance.enemies[index];
        FightManager.Instance.NextPhase();
        
    }
}
