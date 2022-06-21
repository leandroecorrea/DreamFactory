using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAttackHandler : BaseAttackHandler
{
    //protected override void HandleMoveToAttackTargetComplete(object sender, EventArgs e)
    //{
    //    // Damage the current target
    //    // targets[currentTargetIndex++].ApplyEffects(new List<IEffectHandler> { new FeverDebuffHandler() });



    //    // Check for more targets, if any exist then move to those
    //    if (currentTargetIndex < targets.Length)
    //    {
    //        combatRouter.BeginRouting(targets[currentTargetIndex].gameObject);
    //        return;
    //    }

    //    // Otherwise, route back to original position
    //    combatRouter.onRoutingComplete -= HandleMoveToAttackTargetComplete;
    //    combatRouter.onRoutingComplete += HandleReturnToPositionComplete;

    //    combatRouter.BeginRouting(initialPosition);
    //}

    //protected override void HandleReturnToPositionComplete(object sender, EventArgs e)
    //{
    //    combatRouter.onRoutingComplete -= HandleReturnToPositionComplete;
    //    OnCombatActionComplete(this, new ActionPerformedArgs { TargetedUnits = targets });
    //}

    protected override void HandleActionExecution()
    {
        ApplyEffects(combatActionConfig.effectsToApply, targets[currentTargetIndex++]);
    }
}
