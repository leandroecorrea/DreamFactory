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
        CombatEntity currentTarget = targets[0];
        currentTarget.TakeDamage((int)(Damage * (1f + UnityEngine.Random.Range(-0.15f, 0.15f))));
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"{Damage}" };
    }
}