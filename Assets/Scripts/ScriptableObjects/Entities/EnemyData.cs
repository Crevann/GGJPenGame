using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Entities/Enemies")]
public class EnemyData : EntityData
{
    public RootCombat[] rootPool;
}
