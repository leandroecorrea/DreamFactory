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

    protected override void HandleActionExecution()
    {
        targets[currentTargetIndex].TakeDamage(Damage);        
        currentTargetIndex++;
    }
}