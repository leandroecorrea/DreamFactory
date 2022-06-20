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

    public Action<CombatEntity> onAttackAreaTrigger;

    public void OnActionPerformed(object sender, ActionPerformedArgs args)
    {
        onActionPerformed?.Invoke(sender, args);
    }

    public void OnAttackAreaTrigger(CombatEntity entity)
    {
        onAttackAreaTrigger?.Invoke(entity);
    }
    
}
