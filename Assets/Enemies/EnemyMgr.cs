using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMgr : Singleton<EnemyMgr>
{
    [SerializeField] float waitTimeToTurnPage = 1.5f;
    AIController[] enemies;
    void Start()
    {
        enemies = FindObjectsOfType<AIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateAllEnemies() {
        foreach (AIController item in enemies) {
            if (!item.GetComponent<TeleportingObject>().onTopOfBook) continue;
            item.GetComponent<NavMeshAgent>().enabled = false;
            item.GetComponent<RiseUpAndDown>().GoDown();
        }
    }
}
