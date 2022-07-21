using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidAttackHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity targetEntity = targets[currentTargetIndex++];
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);

        ApplyEffects(combatActionConfig.effectsToApply, targetEntity);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { targetEntity }, ActionPerformed = this, feedbackMessage = "" };
    }
}
