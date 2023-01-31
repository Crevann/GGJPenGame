using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemyAI : MonoBehaviour
{
    protected Entity self;
    protected Entity target;
    protected RootCombat currentSelectedRoot;

    private void Awake() {
        self = GetComponent<Entity>();
    }
    public void UseRoot() {
        currentSelectedRoot.SetUser(self);
        currentSelectedRoot.Action();
        //Clear root for next turn
        currentSelectedRoot = null;
    }

    public void Execute() {
        self.DealDamage(self.Damage, target);
    }
    abstract public Root SelectRoot();

    abstract public Entity SelectTarget();

    //TODO Implement stall AI
}
