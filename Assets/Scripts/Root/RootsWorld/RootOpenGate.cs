using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RootOpenGate : RootWorld {
    [SerializeField] protected Animation gate;
    [SerializeField] private UnityEvent useEvent;
    public override void Action() {
        if (gate) {
            gate.Play();
            useEvent.Invoke();
        }
    }
}
