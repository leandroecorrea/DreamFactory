using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackHandler : ICombatAction
{
    public event EventHandler<ActionPerformedArgs> onCombatActionComplete;

    private int fixedDamage = 10;

    public void ExecuteAction(params CombatEntity[] targets)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].TakeDamage(fixedDamage);
            Debug.Log($"Target {targets[i].gameObject.name} took {fixedDamage} damage, its current HP is {targets[i].CurrentHP}");
        }

        onCombatActionComplete?.Invoke(this, new ActionPerformedArgs { TargetedUnits = targets, Message ="Action performed"});
    }
}