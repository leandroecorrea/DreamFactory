using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComaCooldownHandler : BaseEffectHandler
{
    CombatEntity applicant;
    private CombatActionConfig removedAction;

    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {    
        this.applicant = applicant;
        removedAction = applicant.entityConfig.actions.Find(x => x.actionName == "Coma");
        if(removedAction != null)
            applicant.entityConfig.actions.Remove(removedAction);        
    }

    protected override void HandleEffectExpire()
    {
        applicant.entityConfig.actions.Add(removedAction);
        Debug.Log("Coma was added again to actions");
        base.HandleEffectExpire();
    }
}
