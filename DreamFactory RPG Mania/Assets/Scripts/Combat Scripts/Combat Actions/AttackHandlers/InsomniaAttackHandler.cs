public class InsomniaAttackHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[currentTargetIndex++];
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);

        ApplyEffects(combatActionConfig.effectsToApply, currentTarget);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = "" };
    }
}