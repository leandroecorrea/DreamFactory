using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverDebuffHandler : BaseEffectHandler
{
    public override void HandleTurnEnd(CombatEntity applicant, CombatContext combatCtx)
    {
        base.HandleTurnEnd(applicant, combatCtx);

        applicant.TakeDamage(combatEffectConfig.baseEffectiveness);
        Debug.Log($"Target {applicant.gameObject.name} took {combatEffectConfig.baseEffectiveness} fever damage, its current HP is {applicant.CurrentHP}");
    }
}
