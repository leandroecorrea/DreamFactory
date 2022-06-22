using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffectHandler : IEffectHandler
{
    protected int roundsRemaining;
    protected CombatEffectConfig effectConfig;

    public CombatEffectConfig combatEffectConfig { 
        get 
        { 
            return effectConfig; 
        } 

        set
        {
            effectConfig = value;
            roundsRemaining = value.roundDuration;
        }
    }

    public int RemainingRounds { get { return roundsRemaining; } }

    public event EventHandler<EventArgs> onEffectExpire;

    public virtual void HandleOnApply(CombatEntity applicant, CombatContext combatCtx) { }

    public virtual void HandleTurnEnd(CombatEntity applicant, CombatContext combatCtx) { RemoveRemainingRound(); }

    public virtual void HandleTurnStart(CombatEntity applicant, CombatContext combatCtx) { }

    public virtual void RemoveRemainingRound()
    {
        if (combatEffectConfig.doesExpire)
        {
            roundsRemaining -= 1;

            if (RemainingRounds == 0)
            {
                HandleEffectExpire();
            }
        }
    }

    protected virtual void HandleEffectExpire()
    {
        Debug.Log("Effect Expired!");
        onEffectExpire?.Invoke(this, EventArgs.Empty);
    }
}
