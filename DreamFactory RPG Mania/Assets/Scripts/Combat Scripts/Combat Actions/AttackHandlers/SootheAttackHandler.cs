using System.Linq;

public class SootheAttackHandler : BaseAttackHandler
{
    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity targetEntity = targets[currentTargetIndex++];
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);
        var effectsToRemove = targetEntity.EffectsCaused.Where(x => x.IsDebuff);
        foreach(var effect in effectsToRemove)
            targetEntity.AddEffectToRemove(effect);        
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { targetEntity }, ActionPerformed = this, feedbackMessage = "" };
    }
}