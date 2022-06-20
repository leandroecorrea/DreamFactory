using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public CombatEntityConfig entityConfig;

    private List<IEffectHandler> effectsCaused;
    private CombatContext currentTurnCtx;
    [SerializeField] private Animator animator;
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

    private void OnDestroy()
    {
        CombatEventSystem.instance.OnCombatEntityKilled(this, new CombatEntityKilledArgs { entityKilled = this });
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
        animator.SetTrigger("Running");
        Debug.Log("Seteado en running");
        Type combatActionType = Type.GetType(action.actionHandlerClassName);
        ICombatAction combatActionInstance = (ICombatAction)Activator.CreateInstance(combatActionType);
        combatActionInstance.onCombatActionComplete += HandleCombatActionComplete;
        CombatEventSystem.instance.onAttackAreaTrigger += TriggerAttackAnimation;
        combatActionInstance.ExecuteAction(this, target);        
    }

    private void TriggerAttackAnimation(CombatEntity entity)
    {
        animator.ResetTrigger("Running");
        animator.SetTrigger("Attack");
    }

    private void HandleCombatActionComplete(object sender, ActionPerformedArgs e)
    {
        EndTurn(currentTurnCtx);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (IsDead())
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

    public bool IsDead()
    {
        return (CurrentHP <= 0);
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
