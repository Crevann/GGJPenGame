using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField] int pageToTurnTo;
    [SerializeField] LayerMask playerLayer = (LayerMask)9;
    [SerializeField] Vector3 whereToGo = new Vector3(-22.1588745f, 2.77509117f, -11.7599993f);
    [SerializeField] float delayTime = 1;
    float count;
    bool isCounting;
    PlayerMovement p;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting) {
            count+=Time.deltaTime;
            if (count > delayTime) {
                GameMGR.Instance.currentLevel = pageToTurnTo;
                LevelMgr.Instance.OpenToPage(pageToTurnTo);
                isCounting = false;
                p.transform.position = whereToGo;
                p.GetComponent<NavMeshAgent>().destination = whereToGo;
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0) {
            Debug.Log("is in Inventary");
            p = other.GetComponent<PlayerMovement>();
            p.JumpIn(transform.position);
            GetComponent<Collider>().enabled = false;
            isCounting = true;
            count = 0;
        }
    }
}
