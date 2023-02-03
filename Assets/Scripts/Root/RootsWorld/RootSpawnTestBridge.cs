using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawnTestBridge : RootWorld {
    [SerializeField] protected GameObject affectedGO;
    public override void Action() {
        if(affectedGO)
            affectedGO.SetActive(true);
    }
}
