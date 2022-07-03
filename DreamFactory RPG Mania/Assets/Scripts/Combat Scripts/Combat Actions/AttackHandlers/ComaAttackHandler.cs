using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComaAttackHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity targetEntity = targets[currentTargetIndex++];
        var comaEffectToList = new List<CombatEffectConfig> { combatActionConfig.effectsToApply[0] };
        var comaCooldownEffectToList = new List<CombatEffectConfig> { combatActionConfig.effectsToApply[1], combatActionConfig.effectsToApply[2] };
        ApplyEffects(comaEffectToList, targetEntity);
        ApplyEffects(comaCooldownEffectToList, executor);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { targetEntity }, ActionPerformed = this, feedbackMessage = "" };
    }

}
