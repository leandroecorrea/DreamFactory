public class HypnosisDebuffHandler : BaseEffectHandler
{
    CombatEntity applicant;
    int attackPreBuff;

    public override bool IsDebuff => true;

    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
        var hypnosisActionExecution = new HypnosisActionExecution(new BaseActionExecution());
        applicant.SetActionExecution(hypnosisActionExecution);
    }

    protected override void HandleEffectExpire()
    {
        applicant.SetActionExecution(new BaseActionExecution());
        base.HandleEffectExpire();
    }
}