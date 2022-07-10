using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatAction
{
    CombatActionConfig combatActionConfig { get; set; }

    event EventHandler<ActionPerformedArgs> onCombatActionComplete;

    void ExecuteAction(CombatEntity executor, params CombatEntity[] targets);
}

public class ActionPerformedArgs : EventArgs
{
    public CombatEntity[] TargetedUnits { get; set; }
    public ICombatAction ActionPerformed { get; set; }
    public string feedbackMessage { get; set; }
}