using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComaDebuffHandler : BaseEffectHandler
{
    CombatEntity applicant;
    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
        this.applicant = applicant;
        applicant.IsDisabled = true;
        Debug.Log("handling debuff");
    }

    protected override void HandleEffectExpire()
    {
        applicant.IsDisabled = false;
        base.HandleEffectExpire();
    }
}
