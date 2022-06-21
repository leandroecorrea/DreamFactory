using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttackHandler : BaseAttackHandler, ICombatAction
{    
    public int Damage { 
        get
        {
            return (combatActionConfig?.baseEffectiveness ?? 0) + executor.CurrentAttack;
        } 
    }


    //protected override void HandleMoveToAttackTargetComplete(object sender, EventArgs e)
    //{
    //    executor.TriggerAttackAnimation();
    //    executor.onAnimationComplete += DoDamage;
    //    executor.onAnimationComplete += ProcessNextTarget;
    //}

    //public void ProcessNextTarget()
    //{
    //    // Check for more targets, if any exist then move to those
    //    if (currentTargetIndex < targets.Length)
    //    {
    //        combatRouter.BeginRouting(targets[currentTargetIndex].gameObject);
    //        return;
    //    }

    //    // Otherwise, route back to original position
    //    combatRouter.onRoutingComplete -= HandleMoveToAttackTargetComplete;
    //    combatRouter.onRoutingComplete += HandleReturnToPositionComplete;

    //    combatRouter.BeginRouting(initialPosition);
    //    CombatEventSystem.instance.OnActionPerformed(this, new ActionPerformedArgs { TargetedUnits = targets, ActionPerformed = this });
    //}

    protected override void HandleActionExecution()
    {
        targets[currentTargetIndex].TakeDamage(Damage);        
        currentTargetIndex++;
    }

    //protected override void HandleReturnToPositionComplete(object sender, EventArgs e)
    //{
    //    executor.onAnimationComplete -= ProcessNextTarget;
    //    executor.onAnimationComplete -= DoDamage;
    //    executor.TriggerIdleAnimation();
    //    combatRouter.onRoutingComplete -= HandleReturnToPositionComplete;
    //    OnCombatActionComplete(this, new ActionPerformedArgs { TargetedUnits = targets });
    //}
}