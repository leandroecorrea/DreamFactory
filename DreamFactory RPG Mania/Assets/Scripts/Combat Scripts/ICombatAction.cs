using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatAction
{
    event EventHandler<ActionPerformedArgs> onCombatActionComplete;

    void ExecuteAction(params CombatEntity[] targets);
}

public class ActionPerformedArgs : EventArgs
{
    public CombatEntity[] TargetedUnits { get; set; }
    public ICombatAction ActionPerformed { get; set; }
}