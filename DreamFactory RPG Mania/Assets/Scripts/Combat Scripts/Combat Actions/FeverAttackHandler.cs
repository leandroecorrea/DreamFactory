using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverAttackHandler : BaseAttackHandler
{
    protected override void HandleActionExecution()
    {
        ApplyEffects(combatActionConfig.effectsToApply, targets[currentTargetIndex++]);
    }
}
