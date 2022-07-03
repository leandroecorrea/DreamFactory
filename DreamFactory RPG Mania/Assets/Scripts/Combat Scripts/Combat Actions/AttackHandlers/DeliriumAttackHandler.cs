using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliriumAttackHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity targetEntity = targets[currentTargetIndex++];

        ApplyEffects(combatActionConfig.effectsToApply, targetEntity);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { targetEntity }, ActionPerformed = this, feedbackMessage = "" };
    }
}
