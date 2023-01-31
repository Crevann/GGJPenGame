using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


abstract public class RootCombat : Root
{
    protected Entity user;

    public RootCombat SelectedRoot() {
        return this;
    }
    public Entity SetUser(Entity entity) {
        user = entity;
        return user;
    }
}
