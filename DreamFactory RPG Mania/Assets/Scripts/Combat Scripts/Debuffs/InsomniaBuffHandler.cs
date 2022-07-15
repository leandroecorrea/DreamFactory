public class InsomniaBuffHandler : BaseEffectHandler
{
    CombatEntity applicant;

    public override bool IsDebuff => false;

    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
        this.applicant = applicant;
        applicant.DamageCalculationStrategy = x => 0;
        applicant.ApplyEffectsStrategy = x=> { };   
    }

    protected override void HandleEffectExpire()
    {
        applicant.ResetDamageCalculation();
        applicant.ResetStandardEffects();
        
        base.HandleEffectExpire();
    }
}