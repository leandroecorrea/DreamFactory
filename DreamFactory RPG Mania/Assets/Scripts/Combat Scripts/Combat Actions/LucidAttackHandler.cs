using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidAttackHandler : BaseAttackHandler
{
    
    protected override void HandleActionExecution()
    {
        ApplyEffects(combatActionConfig.effectsToApply, targets);
        executor.GetComponent<Animator>().GetBehaviour<Spell_CombatBehaviour>().onSpellAnimationComplete -= HandleActionExecution;
        OnCombatActionComplete(this, new ActionPerformedArgs { TargetedUnits = targets });
    }
    public override void ExecuteAction(CombatEntity executor, params CombatEntity[] targets)
    {
        this.executor = executor;
        this.targets = targets;
        executor.TriggerSpellAnimation();
        //suscribe to an animator event on behaviour finished
        executor.GetComponent<Animator>().GetBehaviour<Spell_CombatBehaviour>().onSpellAnimationComplete += HandleActionExecution;
    }
}
