using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthNumber;
    [SerializeField]Image barHealth;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthNumber.text = GameMGR.Instance.penHP.ToString() + "/" + GameMGR.Instance.penMaxHP.ToString();
        barHealth.fillAmount = (float)GameMGR.Instance.penHP / (float)GameMGR.Instance.penMaxHP;
    }
}
