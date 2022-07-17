using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAttackHandler : BaseAttackHandler
{

    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[currentTargetIndex++];

        ApplyEffects(combatActionConfig.effectsToApply, currentTarget);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = "" };
    }
}
