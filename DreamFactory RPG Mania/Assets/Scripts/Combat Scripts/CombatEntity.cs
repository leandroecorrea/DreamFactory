using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public CombatEntityConfig entityConfig;

    private Dictionary<string, List<IEffectHandler>> effectsCaused;
    private List<IEffectHandler> effectsToRemove;
    private CombatContext currentTurnCtx;
    [SerializeField] private Animator animator;
    public int CurrentHP { get; private set; }    
    private int currentMaxHP;
    public int CurrentMP { get; private set; }
    private int currentMaxMP;

    public int CurrentAttack { get; private set; }
    private int currentAttack;

    private int currentSpeed;

    public event EventHandler<OnTurnCompleteEventArgs> onTurnComplete;
    public event Action onAnimationComplete;

    public void Awake()
    {
        // Initializing Stats
        currentMaxHP = entityConfig.baseHP;
        CurrentHP = currentMaxHP;

        currentMaxMP = entityConfig.baseMP;
        CurrentMP = currentMaxMP;

        currentAttack = entityConfig.baseAttack;
        CurrentAttack = currentAttack;

        currentSpeed = entityConfig.baseSpeed;

        // Initializing Effect Dictionary
        effectsCaused = new Dictionary<string, List<IEffectHandler>>();
    }


    public void OnAnimationComplete()
    {
        onAnimationComplete?.Invoke();
    }


    private void OnDestroy()
    {
        CombatEventSystem.instance.OnCombatEntityKilled(this, new CombatEntityKilledArgs { entityKilled = this });
    }

    public virtual void StartTurn(CombatContext turnContext)
    {
        currentTurnCtx = turnContext;

        if (effectsCaused.Count > 0)
        {
            foreach (KeyValuePair<string, List<IEffectHandler>> effectIdToEffectHandlers in effectsCaused)
            {
                foreach(IEffectHandler effectHandler in effectIdToEffectHandlers.Value)
                {
                    effectHandler.HandleTurnStart(this, currentTurnCtx);
                }
            }
        }

        return;
    }

    public virtual void EndTurn(CombatContext turnContext)
    {
        if (effectsCaused.Count > 0)
        {
            foreach (KeyValuePair<string, List<IEffectHandler>> effectIdToEffectHandlers in effectsCaused)
            {
                foreach (IEffectHandler effectHandler in effectIdToEffectHandlers.Value)
                {
                    effectHandler.HandleTurnEnd(this, currentTurnCtx);
                }
            }
        }

        if (effectsToRemove != null && effectsToRemove.Count > 0)
        {
            foreach (IEffectHandler effectHandlerToRemove in effectsToRemove)
            {
                if (effectsCaused.TryGetValue(effectHandlerToRemove.combatEffectConfig.effectId, out List<IEffectHandler> existingEffectHandlersById))
                {
                    existingEffectHandlersById.Remove(effectHandlerToRemove);
                }
            }
        }
        

        currentTurnCtx = null;
        onTurnComplete?.Invoke(this, new OnTurnCompleteEventArgs { targetEntity = this });

        return;
    }

    public void PerformAction(CombatActionConfig action, params CombatEntity[] target)
    {
        animator.SetTrigger("Running");        
        Type combatActionType = Type.GetType(action.actionHandlerClassName);
        ICombatAction attackHandlerInterface = (ICombatAction)Activator.CreateInstance(combatActionType);
        attackHandlerInterface.combatActionConfig = action;
        attackHandlerInterface.onCombatActionComplete += HandleCombatActionComplete;
        attackHandlerInterface.ExecuteAction(this, target);        
    }

    internal void TriggerIdleAnimation()
    {
        animator.ResetTrigger("Running");
        animator.SetTrigger("Idle");
    }

    public void TriggerAttackAnimation()
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
        animator.SetTrigger("IsHit");
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
            CombatEffectConfig targetEffectConfig = effectHandler.combatEffectConfig;

            // There is already a list of the target effect type, check stacking
            if (effectsCaused.TryGetValue(targetEffectConfig.effectId, out List<IEffectHandler> existingEffects))
            {
                if ((!targetEffectConfig.doesStack && existingEffects.Count > 0) || (targetEffectConfig.doesStack && existingEffects.Count >= targetEffectConfig.maxStackCount))
                {
                    continue;
                }

                effectHandler.onEffectExpire += HandleEffectExpire;
                effectHandler.HandleOnApply(this, currentTurnCtx);

                existingEffects.Add(effectHandler);
            }
            else
            {
                // The effect hasn't been applied yet, so apply it
                effectHandler.onEffectExpire += HandleEffectExpire;
                effectHandler.HandleOnApply(this, currentTurnCtx);

                effectsCaused.Add(targetEffectConfig.effectId, new List<IEffectHandler> { effectHandler });
            }
                
        }

        return;
    }

    public void HandleEffectExpire(object sender, EventArgs e)
    {
        IEffectHandler effectHandlerInterfaceInstance = (IEffectHandler)sender;
        CombatEffectConfig effectConfig = effectHandlerInterfaceInstance.combatEffectConfig;

        if (effectsCaused.TryGetValue(effectConfig.effectId, out List<IEffectHandler> existingHandlers))
        {
            IEffectHandler effectHandlerToRemove = existingHandlers.Find(handler => handler == effectHandlerInterfaceInstance);
            if (effectHandlerToRemove != null)
            {
                if (effectsToRemove == null)
                {
                    effectsToRemove = new List<IEffectHandler>();
                }

                effectsToRemove.Add(effectHandlerToRemove);
            }
        }
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
