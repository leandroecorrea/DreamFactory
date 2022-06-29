using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectHandler
{
    public CombatEffectConfig combatEffectConfig { get; set; }
    public bool IsDebuff { get; }
    public CombatEntity applier { get; set; }

    public int RemainingRounds { get; }

    public event EventHandler<EventArgs> onEffectExpire;

    void InitializeEffect(CombatEffectConfig combatEffectConfig, CombatEntity applier, CombatEntity applicant);

    void HandleOnApply(CombatEntity applicant, CombatContext combatCtx);
    void HandleTurnStart(CombatEntity applicant, CombatContext combatCtx);
    void HandleTurnEnd(CombatEntity applicant, CombatContext combatCtx);
}
