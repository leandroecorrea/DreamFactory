using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public CombatEntityConfig entityConfig;

    private List<IEffectHandler> effectsCaused;
    public int CurrentHP { get; private set; }    
    private int currentMaxHP;
    public int CurrentMP { get; private set; }
    private int currentMaxMP;
    private int currentAttack;
    private int currentSpeed;

    public event EventHandler<OnTurnCompleteEventArgs> onTurnComplete;

    public void Awake()
    {
        currentMaxHP = entityConfig.baseHP;
        CurrentHP = currentMaxHP;

        currentMaxMP = entityConfig.baseMP;
        CurrentMP = currentMaxMP;

        currentAttack = entityConfig.baseAttack;
        currentSpeed = entityConfig.baseSpeed;
    }

    public void PerformAction(CombatActionConfig action, params CombatEntity[] target)
    {
        Type combatActionType = Type.GetType(action.actionHandlerClassName);
        ICombatAction combatActionInstance = (ICombatAction)Activator.CreateInstance(combatActionType);
        combatActionInstance.onCombatActionComplete += HandleCombatActionComplete;
        combatActionInstance.ExecuteAction(target);
    }

    private void HandleCombatActionComplete(object sender, ActionPerformedArgs e)
    {
        onTurnComplete?.Invoke(this, new OnTurnCompleteEventArgs { targetEntity = this });         
    }

    public virtual void StartTurn(CombatContext turnContext)
    {
        Debug.LogError("StartTurn Not Implemented");
        return;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            // TODO: Remove this once hooked up to animation event
            HandleEntityDeath();
        }
    }

    public virtual void ApplyEffects(List<IEffectHandler> affectsToApply)
    {
        Debug.LogError("ApplyEffects Not Implemented");
        return;
    }

    // TODO: Hook this up via an animation event to death animation
    public void HandleEntityDeath()
    {
        GameObject.Destroy(gameObject);
    }
}

public class OnTurnCompleteEventArgs : EventArgs
{
    public CombatEntity targetEntity { get; set; }
} 
