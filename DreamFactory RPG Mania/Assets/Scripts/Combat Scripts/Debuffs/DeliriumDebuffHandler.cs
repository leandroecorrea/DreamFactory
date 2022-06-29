using System;
using UnityEngine;

public class DeliriumDebuffHandler : BaseEffectHandler
{
    CombatEntity applicant;
    int attackPreBuff;

    public override bool IsDebuff => true;

    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
        this.applicant = applicant;
        attackPreBuff = applicant.entityConfig.baseAttack;

        applicant.CurrentAttack = Convert.ToInt32(applicant.CurrentAttack * 0.8f);
        Debug.Log($"Target {applicant.gameObject.name} current attack is {applicant.CurrentAttack}, doubled by Lucid spell");
    }

    protected override void HandleEffectExpire()
    {
        applicant.CurrentAttack = attackPreBuff;
        base.HandleEffectExpire();
    }
}