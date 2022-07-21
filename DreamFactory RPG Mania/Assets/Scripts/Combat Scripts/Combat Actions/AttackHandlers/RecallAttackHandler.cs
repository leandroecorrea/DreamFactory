public class RecallAttackHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity targetEntity = targets[currentTargetIndex++];
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);
        var freezeHandler = targetEntity.EffectsCaused.Find(x => x.combatEffectConfig.displayName == "Freeze");
        if (freezeHandler != null)
        {
            targetEntity.AddEffectToRemove(freezeHandler);
            targetEntity.entityConfig.actions.Add(targetEntity.LastExecutedAction);
        }
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { targetEntity }, ActionPerformed = this, feedbackMessage = "" };
    }
}
