using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidBuffHandler : BaseEffectHandler
{
    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
        roundsRemaining = 2;
        applicant.CurrentAttack *= 2;
        Debug.Log($"Target {applicant.gameObject.name} current attack is {applicant.CurrentAttack}, doubled by Lucid spell");
    }
}
