using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public CombatEntityConfig entityConfig;

    private Dictionary<string, List<IEffectHandler>> effectsCaused;
    private List<IEffectHandler> effectsToRemove;
    private CombatContext currentTurnCtx;
    private ActionExecution actionExecution;

    internal void ResetStandardEffects()
    {
        ApplyEffectsStrategy = ApplyEffectsStandard;
    }

    [SerializeField] private Animator animator;
    public Action<int> TakeDamageStrategy;
    public Action<List<IEffectHandler>> ApplyEffectsStrategy;
    public CombatActionConfig LastExecutedAction { get; set; }

    public int CurrentHP { get; set; }
    private int currentMaxHP;
    public bool IsDisabled { get; set; } = false;

    public int CurrentMP { get; set; }
    private int currentMaxMP;

    public int CurrentAttack { get; set; }
    public List<IEffectHandler> EffectsCaused
    {
        get {
            var list = new List<IEffectHandler>();
            foreach(var listOfEffects in effectsCaused.Values)
            {
                list.AddRange(listOfEffects);
            }
            return list;
        }
    }

    private int currentAttack;
    
    public event EventHandler<OnTurnCompleteEventArgs> onTurnComplete;
    public event Action onAnimationComplete;

    public void Awake()
    {
        TakeDamageStrategy = TakeStandardDamage;
        ApplyEffectsStrategy = ApplyEffectsStandard;
        // Initializing Stats
        currentMaxHP = entityConfig.baseHP;
        CurrentHP = currentMaxHP;

        currentMaxMP = entityConfig.baseMP;
        CurrentMP = currentMaxMP;

        currentAttack = entityConfig.baseAttack;
        CurrentAttack = currentAttack;
        actionExecution = new BaseActionExecution();
        // Initializing Effect Dictionary
        effectsCaused = new Dictionary<string, List<IEffectHandler>>();
    }


    public void TakeDamage(int damage)
    {
        TakeDamageStrategy?.Invoke(damage);
    }
    public void ApplyEffects(List<IEffectHandler> effectHandlers)
    {
        ApplyEffectsStrategy?.Invoke(effectHandlers);   
    }

    public void SetActionExecution(ActionExecution actionExecution)
    {
        this.actionExecution = actionExecution;
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
                foreach (IEffectHandler effectHandler in effectIdToEffectHandlers.Value)
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
                AddEffectToRemove(effectHandlerToRemove);
            }
            effectsToRemove = null;
        }


        currentTurnCtx = null;
        onTurnComplete?.Invoke(this, new OnTurnCompleteEventArgs { targetEntity = this });

        return;
    }

    public void AddEffectToRemove(IEffectHandler effectHandlerToRemove)
    {
        if (effectsCaused.TryGetValue(effectHandlerToRemove.combatEffectConfig.effectId, out List<IEffectHandler> existingEffectHandlersById))
        {
            existingEffectHandlersById.Remove(effectHandlerToRemove);
        }
    }

    public void PerformAction(CombatActionConfig action, params CombatEntity[] target)
    {        
        var actionRequest = new CombatActionRequest { ActionChosen = action, CurrentEntity = this, Targets = target };
        actionExecution.Execute(actionRequest);        
    }

    public void UpdateEntityMP(int newMP)
    {
        this.CurrentMP = newMP;
    }

    public void TriggerIdleAnimation()
    {
        animator.SetBool("IsMoving", false);
    }

    public void TriggerRunningAnimation()
    {
        animator.SetBool("IsMoving", true);
    }

    public void TriggerSpellAnimation()
    {
        animator.SetTrigger("SPELL");
    }

    public void TriggerReviveAnimation()
    {
        throw new NotImplementedException();
    }

    public void OnAnimationComplete()
    {
        onAnimationComplete?.Invoke();
    }

    public void HandleCombatActionComplete(object sender, ActionPerformedArgs e)
    {
        EndTurn(currentTurnCtx);
    }

    public void ResetTakeDamage()
    {
        TakeDamageStrategy = TakeStandardDamage;
    }
    
    protected virtual void TakeStandardDamage(int damage)
    {
        animator.SetTrigger("Damaged");

        CurrentHP -= damage;
        CombatEventSystem.instance.OnCombatEntityDamage(this, new CombatEntityDamagedArgs { damageTaken = damage });

        if (IsDead())
        {
            // TODO: Remove this once hooked up to animation event
            HandleEntityDeath();
        }
    }
    public void Heal(int healAmount)
    {
        CurrentHP = CurrentHP + healAmount > entityConfig.baseHP? entityConfig.baseHP : CurrentHP + healAmount;
    }

    

    protected virtual void ApplyEffectsStandard(List<IEffectHandler> affectsToApply)
    {
        foreach (IEffectHandler effectHandler in affectsToApply)
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
    public void SkipTurn(CombatContext turnContext)
    {
        Debug.Log("Player is disabled");
        EndTurn(turnContext);        
    }
    public bool IsDead()
    {
        return (CurrentHP <= 0);
    }

    // TODO: Hook this up via an animation event to death animation
    public virtual void HandleEntityDeath()
    {
        //if its player controllable entity, we may want to keep alive rendering but dead, in case it is revived
        GameObject.Destroy(gameObject);
    }
}

public class OnTurnCompleteEventArgs : EventArgs
{
    public CombatEntity targetEntity { get; set; }
}
