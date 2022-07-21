using UnityEngine;

public class RemAttackHandler : BaseAttackHandler
{
    

    protected override ActionPerformedArgs HandleActionExecution()
    {
        CombatEntity currentTarget = targets[0];
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);
        currentTarget.Heal((int)(combatActionConfig.baseEffectiveness * (1f + Random.Range(-0.15f, 0.15f))));
        return new ActionPerformedArgs { TargetedUnits = new CombatEntity[] { currentTarget }, ActionPerformed = this, feedbackMessage = $"Healed: {combatActionConfig.baseEffectiveness}" };
    }
}