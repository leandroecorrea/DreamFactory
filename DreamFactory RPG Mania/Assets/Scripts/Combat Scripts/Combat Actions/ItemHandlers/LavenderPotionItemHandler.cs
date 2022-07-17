public class LavenderPotionItemHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[0];        
        currentTarget.RestoreMP(combatActionConfig.baseEffectiveness);
        CombatEventSystem.instance.OnItemUsedInCombat(combatActionConfig.actionHandlerClassName);

        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"Healed: {combatActionConfig.baseEffectiveness}" };
    }
}