using System;



public class CoffeeItemHandler : BaseAttackHandler
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
        currentTarget = targets[currentTargetIndex++];
        currentTarget.TakeDamage(Damage);
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