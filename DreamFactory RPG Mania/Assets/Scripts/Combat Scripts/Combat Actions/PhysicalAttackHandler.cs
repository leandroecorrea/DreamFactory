using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttackHandler : BaseAttackHandler
{    
    public int Damage { 
        get
        {
            return (combatActionConfig?.baseEffectiveness ?? 0) + executor.CurrentAttack;
        } 
    }

    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[currentTargetIndex++];
        currentTarget.TakeDamage(Damage);

        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"{Damage}" };
    }
}