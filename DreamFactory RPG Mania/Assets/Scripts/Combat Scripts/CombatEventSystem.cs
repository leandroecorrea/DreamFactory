using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEventSystem : MonoBehaviour
{
    public static CombatEventSystem instance;
    private void Awake()
    {
        instance = this;
    }

    public event EventHandler<ActionPerformedArgs> onActionPerformed;
    public event EventHandler<CombatEntityKilledArgs> onCombatEntityKilled;

    public void OnActionPerformed(object sender, ActionPerformedArgs args)
    {
        onActionPerformed?.Invoke(sender, args);
    }

    public void OnCombatEntityKilled(object sender, CombatEntityKilledArgs args)
    {
        onCombatEntityKilled?.Invoke(sender, args);
    }
}

public class CombatEntityKilledArgs : EventArgs
{
    public CombatEntity entityKilled;
}