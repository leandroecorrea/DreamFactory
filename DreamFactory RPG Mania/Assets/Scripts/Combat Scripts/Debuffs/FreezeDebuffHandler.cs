public class FreezeDebuffHandler : BaseEffectHandler
{
    CombatEntity applicant;
    CombatActionConfig configRemoved;
    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
        if (applicant.LastExecutedAction == null)
            return;
        configRemoved = applicant.LastExecutedAction;
        applicant.entityConfig.actions.Remove(configRemoved);
    }

    protected override void HandleEffectExpire()
    {
        applicant.entityConfig.actions.Add(configRemoved);        
        base.HandleEffectExpire();
    }
}
