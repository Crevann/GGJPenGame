using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]Button[] buttons;
    TextMeshProUGUI[] namesRoots;
    RectTransform myrect;
    Vector3 oldRect;
    [SerializeField] RectTransform target;
    [SerializeField]float speed;
    bool isOpened = false;

    void Start()
    {
        myrect = GetComponent<RectTransform>();
        oldRect = myrect.localPosition;
        namesRoots = new TextMeshProUGUI[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            namesRoots[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        for (int i = 0; i < Inventory.Instance.roots.Length; i++)
        {
           if (!Inventory.Instance.roots[i])
           {
               buttons[i].gameObject.SetActive(false);
               continue;
           }
           else buttons[i].gameObject.SetActive(true);
           namesRoots[i].text = Inventory.Instance.roots[i].data.root;
           if (Inventory.Instance.roots[i].data.context == GameMGR.Instance.CurrentState)
           {
                buttons[i].interactable = true;
           }
           else
           {
                buttons[i].interactable = false;
           }

           if(Inventory.Instance.roots[i].data.context == GameState.World)
           {
                namesRoots[i].color = new Color(255, 215, 0, 255);
           }
           else
           {
                namesRoots[i].color = new Color(255, 255, 255, 255);
            }
        }

        if (isOpened)
        {
            myrect.localPosition = new Vector3(Mathf.Lerp(myrect.localPosition.x, target.localPosition.x, speed * Time.deltaTime), myrect.localPosition.y, myrect.localPosition.z);
        }
        else
        {
            myrect.localPosition = new Vector3(Mathf.Lerp(myrect.localPosition.x, oldRect.x, speed * Time.deltaTime), myrect.localPosition.y, myrect.localPosition.z);
        }
    }

    public void MoveInventory()
    {
        isOpened = !isOpened;
    }

    public void SelectRoot(int index)
    {
        
        if (Inventory.Instance.roots[index].data.context == GameState.World)
        {
            Inventory.Instance.UseWord(index);
            Inventory.Instance.RemoveRoot(index);
        }
        else if(Inventory.Instance.roots[index].data.context == GameState.Fight)
        {
            FightManager.Instance.PlayerFightingController.currentSelectedRoot = (RootCombat)Inventory.Instance.SelectRoot(index);
            FightManager.Instance.PlayerFightingController.Setup();
            if(((RootCombatData)Inventory.Instance.roots[index].data).avaiableTargets != Targettables.Single)
            {
                FightManager.Instance.NextPhase();
            }
        }
    }
    
}