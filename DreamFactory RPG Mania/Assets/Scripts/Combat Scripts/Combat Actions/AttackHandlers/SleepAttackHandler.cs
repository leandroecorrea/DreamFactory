using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepAttackHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[0];
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);
        currentTarget.TriggerReviveAnimation();
        RestoreHPFor(currentTarget);        
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"{currentTarget.entityConfig.Name} is back!" };
    }

    private void RestoreHPFor(CombatEntity currentTarget)
    {
        currentTarget.CurrentHP = currentTarget.entityConfig.baseHP;
    }
}
