using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MintPotionItemHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {        
        CombatEntity currentTarget = targets[0];
        currentTarget.Heal(combatActionConfig.baseEffectiveness);
        CombatEventSystem.instance.OnItemUsedInCombat(combatActionConfig.actionHandlerClassName);
        
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"Healed: {combatActionConfig.baseEffectiveness}" };
    }
}
