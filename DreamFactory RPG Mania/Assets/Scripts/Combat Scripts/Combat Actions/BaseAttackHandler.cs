using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackHandler : ICombatAction
{
    public event EventHandler<ActionPerformedArgs> onCombatActionComplete;

    public CombatActionConfig combatActionConfig { get; set; }
    protected CombatRouter combatRouter;
    protected CombatEntity executor;
    protected CombatEntity[] targets;

    protected Vector3 initialPosition;
    protected int currentTargetIndex;

    public void ExecuteAction(CombatEntity executor, params CombatEntity[] targets)
    {
        this.executor = executor;
        this.targets = targets;
        this.initialPosition = executor.gameObject.transform.position;
        this.currentTargetIndex = 0;

        combatRouter = executor.gameObject.GetComponent<CombatRouter>();
        if (combatRouter == null)
        {
            Debug.LogError($"Couldn't find Combat Router for {combatRouter.gameObject.name}");
            return;
        }

        combatRouter.onRoutingComplete += HandleMoveToAttackTargetComplete;
        combatRouter.BeginRouting(targets[0].gameObject);
    }
    public void ApplyEffects(List<CombatEffectConfig> effectsToApply, params CombatEntity[] targets)
    {
        foreach(CombatEntity targetEntity in targets)
        {
            List<IEffectHandler> effectHandlerInstances = new List<IEffectHandler>();

            foreach(CombatEffectConfig targetEffect in effectsToApply)
            {
                Type combatEffectType = Type.GetType(targetEffect.effectHandlerClassName);
                IEffectHandler effectHandlerInterfaceInstance = (IEffectHandler)Activator.CreateInstance(combatEffectType);

                effectHandlerInterfaceInstance.combatEffectConfig = targetEffect;
                effectHandlerInstances.Add(effectHandlerInterfaceInstance);
            }

            targetEntity.ApplyEffects(effectHandlerInstances);
        }
    }

    protected void HandleMoveToAttackTargetComplete(object sender, EventArgs e)
    {
        CombatEntity targetCombatEntity = ((CombatRouter)sender).GetComponent<CombatEntity>();
        
        targetCombatEntity.TriggerAttackAnimation();
        targetCombatEntity.onAnimationComplete += HandleAttackAnimationComplete;
    }

    protected void ProcessNextTarget()
    {
        // Check for more targets, if any exist then move to those
        if (currentTargetIndex < targets.Length)
        {
            combatRouter.BeginRouting(targets[currentTargetIndex].gameObject);
            return;
        }

        // Otherwise, route back to original position
        combatRouter.onRoutingComplete -= HandleMoveToAttackTargetComplete;
        combatRouter.onRoutingComplete += HandleReturnToPositionComplete;

        combatRouter.BeginRouting(initialPosition);
        CombatEventSystem.instance.OnActionPerformed(this, new ActionPerformedArgs { TargetedUnits = targets, ActionPerformed = this });
    }

    protected void HandleAttackAnimationComplete()
    {
        HandleActionExecution();
        ProcessNextTarget();
    }

    protected abstract void HandleActionExecution();

    protected void HandleReturnToPositionComplete(object sender, EventArgs e)
    {
        executor.onAnimationComplete -= HandleAttackAnimationComplete;
        combatRouter.onRoutingComplete -= HandleReturnToPositionComplete;

        executor.TriggerIdleAnimation();
        
        OnCombatActionComplete(this, new ActionPerformedArgs { TargetedUnits = targets });
    }

    public void OnCombatActionComplete(object sender, ActionPerformedArgs args)
    {
        onCombatActionComplete?.Invoke(sender, args);
    }

    
}
