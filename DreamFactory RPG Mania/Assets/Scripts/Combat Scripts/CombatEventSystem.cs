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
    public event EventHandler<CombatEntityDamagedArgs> onCombatEntityDamaged;
    public event EventHandler<CombatEntityKilledArgs> onCombatEntityKilled;
    public Action<string> onItemUsedInCombat;
    public Action<CombatResult> onCombatFinished;


    public Action<CombatEntity> onAttackAreaTrigger;



    public void OnCombatFinished(CombatResult result)
    {
        onCombatFinished?.Invoke(result);
    }
    public void OnItemUsedInCombat(string itemHandler)
    {
        onItemUsedInCombat?.Invoke(itemHandler); 
    }
    public void OnActionPerformed(object sender, ActionPerformedArgs args)
    {
        onActionPerformed?.Invoke(sender, args);
    }

    public void OnAttackAreaTrigger(CombatEntity entity)
    {
        onAttackAreaTrigger?.Invoke(entity);
    }
    
    public void OnCombatEntityDamage(object sender, CombatEntityDamagedArgs args)
    {
        onCombatEntityDamaged?.Invoke(sender, args);
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

public class CombatEntityDamagedArgs : EventArgs
{
    public int damageTaken;
}
public enum CombatResult
{
    WIN, LOSE
}