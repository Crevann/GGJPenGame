using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootOpenGate : RootWorld {
    [SerializeField] protected Animation gate;
    public override void Action() {
        if (gate)
            gate.Play();
    }
}
