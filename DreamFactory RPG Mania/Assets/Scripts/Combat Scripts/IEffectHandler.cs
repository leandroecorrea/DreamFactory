using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectHandler
{
    void HandleOnApply(CombatEntity applicant, CombatContext combatCtx);
    void HandleTurnStart(CombatEntity applicant, CombatContext combatCtx);
    void HandleTurnEnd(CombatEntity applicant, CombatContext combatCtx);
}
