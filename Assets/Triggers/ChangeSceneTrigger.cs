using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChangeSceneTrigger : MonoBehaviour {
    [SerializeField] int pageToTurnTo;
    [SerializeField] LayerMask playerLayer = (LayerMask)9;
    [SerializeField] Vector3 whereToGo = new Vector3(-22.1588745f, 2.77509117f, -11.7599993f);
    PlayerMovement p;


    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }
    private void OnEnable() {
        GetComponent<Collider>().enabled = true;
        
    }
    private void OnTriggerEnter(Collider other) {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0) {
            p = other.GetComponent<PlayerMovement>();
            GetComponent<Collider>().enabled = false;
            Inventory.Instance.ClearInventory();//TODO remove
            PageManager.Instance.OpenToPage(pageToTurnTo);
            GameMGR.Instance.currentLevel = pageToTurnTo;
            p.JumpIn(transform.position, whereToGo);
        }
    }
}
