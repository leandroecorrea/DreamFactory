using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetStrategy : ScriptableObject, ITargetStrategy
{
    public abstract List<CombatEntity> GetTargets(CombatContext context);
}
