using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverDebuffHandler : IEffectHandler
{
    private int baseDamage = 10;

    public void HandleOnApply(CombatEntity applicant, CombatContext combatCtx) { }

    public void HandleTurnStart(CombatEntity applicant, CombatContext combatCtx) { }

    public void HandleTurnEnd(CombatEntity applicant, CombatContext combatCtx)
    {
        applicant.TakeDamage(baseDamage);
        Debug.Log($"Target {applicant.gameObject.name} took {baseDamage} fever damage, its current HP is {applicant.CurrentHP}");
    }
}
