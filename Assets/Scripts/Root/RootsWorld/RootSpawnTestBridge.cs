using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawnTestBridge : RootWorld {
    public override void Action() {
        affectedGO.SetActive(true);
    }
}
