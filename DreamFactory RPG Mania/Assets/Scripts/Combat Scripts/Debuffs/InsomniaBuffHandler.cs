public class InsomniaBuffHandler : BaseEffectHandler
{
    CombatEntity applicant;

    public override bool IsDebuff => false;

    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
        this.applicant = applicant;
        applicant.TakeDamageStrategy = x => { };
        applicant.ApplyEffectsStrategy = x=> { };   
    }

    protected override void HandleEffectExpire()
    {
        applicant.ResetTakeDamage();
        applicant.ResetStandardEffects();
        
        base.HandleEffectExpire();
    }
}