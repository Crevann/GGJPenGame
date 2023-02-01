using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemyAI : MonoBehaviour
{
    protected Entity self;
    protected Entity target;
    protected RootCombat currentSelectedRoot;
    public RootCombat CurrentSelectedRoot => currentSelectedRoot;

    private void Awake() {
        self = GetComponent<Entity>();
    }
    public void UseRoot() {
        currentSelectedRoot.SetUser(self);
        currentSelectedRoot.Action();
    }

    public void Execute() {
        self.DealDamage(self.Damage, target);
    }
    abstract public Root SelectRoot();

    abstract public Entity SelectTarget();

    //TODO Implement stall AI
}
