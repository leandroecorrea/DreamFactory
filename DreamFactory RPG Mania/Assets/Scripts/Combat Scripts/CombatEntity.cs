using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public CombatEntityConfig entityConfig;

    private List<IEffectHandler> effectsCaused;
    private CombatContext currentTurnCtx;

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

    public virtual void StartTurn(CombatContext turnContext)
    {
        currentTurnCtx = turnContext;

        foreach (IEffectHandler effectHandler in effectsCaused)
        {
            effectHandler.HandleTurnStart(this, currentTurnCtx);
        }

        return;
    }

    public virtual void EndTurn(CombatContext turnContext)
    {
        if (effectsCaused != null && effectsCaused.Count > 0)
        {
            foreach (IEffectHandler effectHandler in effectsCaused)
            {
                effectHandler.HandleTurnEnd(this, currentTurnCtx);
            }
        }

        currentTurnCtx = null;
        onTurnComplete?.Invoke(this, new OnTurnCompleteEventArgs { targetEntity = this });

        return;
    }

    public void PerformAction(CombatActionConfig action, params CombatEntity[] target)
    {
        Type combatActionType = Type.GetType(action.actionHandlerClassName);
        ICombatAction combatActionInstance = (ICombatAction)Activator.CreateInstance(combatActionType);
        combatActionInstance.onCombatActionComplete += HandleCombatActionComplete;
        combatActionInstance.ExecuteAction(this, target);
    }

    private void HandleCombatActionComplete(object sender, ActionPerformedArgs e)
    {
        EndTurn(currentTurnCtx);
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
        foreach(IEffectHandler effectHandler in affectsToApply)
        {
            effectHandler.HandleOnApply(this, currentTurnCtx);
        }

        if (effectsCaused == null)
        {
            effectsCaused = new List<IEffectHandler>();
        }

        effectsCaused.AddRange(affectsToApply);
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
