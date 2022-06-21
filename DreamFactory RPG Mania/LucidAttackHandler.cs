using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidAttackHandler : BaseAttackHandler, ICombatAction
{
    public CombatActionConfig combatActionConfig { get ; set; }

    public event EventHandler<ActionPerformedArgs> onCombatActionComplete;

    public void ExecuteAction(CombatEntity executor, params CombatEntity[] targets)
    {
        throw new NotImplementedException();
    }
}
