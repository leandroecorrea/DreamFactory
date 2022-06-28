public class InsomniaBuffHandler : BaseEffectHandler
{
    CombatEntity applicant;
    
    public override void HandleOnApply(CombatEntity applicant, CombatContext combatCtx)
    {
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