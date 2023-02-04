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
        switch (((RootCombatData)currentSelectedRoot.data).avaiableTargets) {
            case Targettables.User:
                self.DealDamage(((RootCombatData)currentSelectedRoot.data).damage,
                    self,
                    ((RootCombatData)currentSelectedRoot.data).stun,
                    ((RootCombatData)currentSelectedRoot.data).weakness,
                    ((RootCombatData)currentSelectedRoot.data).showPopup,
                    ((RootCombatData)currentSelectedRoot.data).multiplier,
                    ((RootCombatData)currentSelectedRoot.data).counter);
                self.SpawnEffect(((RootCombatData)currentSelectedRoot.data).effect, self);
                AudioManager.instance.PlayOneShot(((RootCombatData)currentSelectedRoot.data).SFX, target.transform.position);
                break;
            case Targettables.Single:
                self.DealDamage(((RootCombatData)currentSelectedRoot.data).damage,
                    target,
                    ((RootCombatData)currentSelectedRoot.data).stun,
                    ((RootCombatData)currentSelectedRoot.data).weakness,
                    ((RootCombatData)currentSelectedRoot.data).showPopup,
                    ((RootCombatData)currentSelectedRoot.data).multiplier,
                    ((RootCombatData)currentSelectedRoot.data).counter);
                self.SpawnEffect(((RootCombatData)currentSelectedRoot.data).effect, target);
                AudioManager.instance.PlayOneShot(((RootCombatData)currentSelectedRoot.data).SFX, target.transform.position);
                break;
            case Targettables.Multiple:
                self.DealDamage(((RootCombatData)currentSelectedRoot.data).damage,
                    target,
                    ((RootCombatData)currentSelectedRoot.data).stun,
                    ((RootCombatData)currentSelectedRoot.data).weakness,
                    ((RootCombatData)currentSelectedRoot.data).showPopup,
                    ((RootCombatData)currentSelectedRoot.data).multiplier,
                    ((RootCombatData)currentSelectedRoot.data).counter);
                self.SpawnEffect(((RootCombatData)currentSelectedRoot.data).effect, target);
                AudioManager.instance.PlayOneShot(((RootCombatData)currentSelectedRoot.data).SFX, target.transform.position);
                break;
            default:
                break;
        }
    }
    abstract public Root SelectRoot();

    abstract public Entity SelectTarget();

    //TODO Implement stall AI
}
