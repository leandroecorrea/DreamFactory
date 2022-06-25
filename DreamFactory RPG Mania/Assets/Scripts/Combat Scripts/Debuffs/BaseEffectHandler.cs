using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffectHandler : IEffectHandler
{
    protected int roundsRemaining;
    protected CombatEffectConfig effectConfig;

    protected CombatEntity _applier;
    public CombatEntity applier { 
        get { return _applier; } 
        set { _applier = value; } 
    }

    public CombatEffectConfig combatEffectConfig { 
        get { return effectConfig; } 
        set { effectConfig = value; }
    }

    public int RemainingRounds { get { return roundsRemaining; } }

    public event EventHandler<EventArgs> onEffectExpire;

    public void InitializeEffect(CombatEffectConfig combatEffectConfig, CombatEntity applier, CombatEntity applicant)
    {
        this.applier = applier;
        this.combatEffectConfig = combatEffectConfig;

        // Handle Round Initialization
        // If the applier is the same as the applicant, add an extra round to ensure expected duration
        roundsRemaining = combatEffectConfig.roundDuration;

        if (applier == applicant)
        {
            roundsRemaining += 1;
        }
    }

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
