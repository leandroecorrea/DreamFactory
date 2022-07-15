using System;

public abstract class SpellBaseAttackHandler : BaseAttackHandler
{
    protected CombatEntity currentTarget;
    public int Damage
    {
        get
        {
            return (combatActionConfig?.baseEffectiveness ?? 0) + executor.CurrentAttack;
        }
    }
    protected override void HandleReturnToPositionComplete(object sender, EventArgs e)
    {
        executor.onAnimationComplete -= HandleAttackAnimationComplete;
        combatRouter.onRoutingComplete -= HandleReturnToPositionComplete;
        combatRouter.RestoreRotation();
        executor.TriggerIdleAnimation();
    }
}