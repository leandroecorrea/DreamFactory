using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyAttackHandler : BaseAttackHandler
{
    CombatEntity currentTarget;
    public int Damage
    {
        get
        {
            return (combatActionConfig?.baseEffectiveness ?? 0) + executor.CurrentAttack;
        }
    }

    protected override ActionPerformedArgs HandleActionExecution()
    {
        currentTarget = targets[0];
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);
        currentTarget.TakeDamage((int)(Damage * (1f + UnityEngine.Random.Range(-0.15f, 0.15f))));
        currentTarget.onAnimationComplete += OnTargetAnimationComplete;
        HandleReturnToPositionComplete(this, EventArgs.Empty);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"{Damage}" };
    }

    protected override void HandleReturnToPositionComplete(object sender, EventArgs e)
    {
        executor.onAnimationComplete -= HandleAttackAnimationComplete;
        combatRouter.onRoutingComplete -= HandleReturnToPositionComplete;
        combatRouter.RestoreRotation();
        executor.TriggerIdleAnimation();
    }

    public void OnTargetAnimationComplete()
    {
        currentTarget.onAnimationComplete -= OnTargetAnimationComplete;
        OnCombatActionComplete(this, new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { targets[0] }, ActionPerformed = this, feedbackMessage = $"{Damage}" });
    }
}
