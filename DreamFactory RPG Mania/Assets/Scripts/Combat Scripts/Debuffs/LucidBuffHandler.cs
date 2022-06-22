using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidBuffHandler : BaseEffectHandler
{
    CombatEntity applicant;
    int attackPreBuff;
    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {        
        this.applicant = applicant;
        attackPreBuff = applicant.entityConfig.baseAttack;
        roundsRemaining = 2;
        applicant.CurrentAttack *= 2;        
        Debug.Log($"Target {applicant.gameObject.name} current attack is {applicant.CurrentAttack}, doubled by Lucid spell");        
    }

    protected override void HandleEffectExpire()
    {
        applicant.CurrentAttack = attackPreBuff;
        base.HandleEffectExpire();
    }    
    
}
