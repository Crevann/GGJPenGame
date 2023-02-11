using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChangeSceneTrigger : MonoBehaviour {
    [SerializeField] int pageToTurnTo;
    [SerializeField] LayerMask playerLayer = (LayerMask)9;
    [SerializeField] Vector3 whereToGo = new Vector3(-22.1588745f, 2.77509117f, -11.7599993f);
    bool isCounting;
    PlayerMovement p;


    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(isCounting && PageManager.Instance.CurrentPage == pageToTurnTo) {
            GameMGR.Instance.currentLevel = pageToTurnTo;
            p.JumpOut(whereToGo);
            isCounting = false;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0) {
            p = other.GetComponent<PlayerMovement>();
            p.JumpIn(transform.position);
            GetComponent<Collider>().enabled = false;
            Inventory.Instance.ClearInventory();//TODO remove
            isCounting = true;
                //TODO move camera
            PageManager.Instance.OpenToPage(pageToTurnTo);
        }
    }
}
