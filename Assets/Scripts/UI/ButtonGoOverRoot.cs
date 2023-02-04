using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGoOverRoot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private int index;
    [SerializeField] TextMeshProUGUI rootDescription;
    [SerializeField] TextMeshProUGUI rootName;
    public void OnPointerEnter(PointerEventData eventData) {
        if (Inventory.Instance.roots[index]) {
            Debug.Log("Inside button");
            rootDescription.text = ((RootCombatData)Inventory.Instance.roots[index].data).description;
            rootName.text = ((RootCombatData)Inventory.Instance.roots[index].data).root;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("Outside button");
        rootDescription.text = "";
        rootName.text = "";
    }
}
