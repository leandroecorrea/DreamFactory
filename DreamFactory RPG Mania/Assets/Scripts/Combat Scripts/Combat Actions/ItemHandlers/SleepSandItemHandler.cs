public class SleepSandItemHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[0];
        currentTarget.TriggerReviveAnimation();
        RestoreHPFor(currentTarget);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"{currentTarget.entityConfig.Name} is back!" };
    }

    private void RestoreHPFor(CombatEntity currentTarget)
    {
        currentTarget.CurrentHP = currentTarget.entityConfig.baseHP;
    }
}