public class RemAttackHandler : BaseAttackHandler
{
    

    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[0];
        currentTarget.Heal(combatActionConfig.baseEffectiveness);
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"Healed: {combatActionConfig.baseEffectiveness}" };
    }
}