using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Entities/Entity")]
public class EntityData : ScriptableObject
{
    public string entityName;
    public int health;
}
