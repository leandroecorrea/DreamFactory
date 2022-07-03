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

    public virtual void ExecuteAction(CombatEntity executor, params CombatEntity[] targets)
    {
        InitializeHandler(executor, targets);
        executor.UpdateEntityMP(executor.CurrentMP - combatActionConfig.requireMana);
        if (combatRouter == null)
            return;        
        // Handle Routing
        if (combatActionConfig.requireRouting)
        {
            HandleRouting(executor, targets);
        }
        else
        {
            // For attacks that don't need routing (I.e far range magic spell, immediately execute)
            var router = executor.GetComponent<CombatRouter>();
            router.MakeLookTo(targets);
            HandleAttackComplete(router, EventArgs.Empty);
        }
    }

    private void HandleRouting(CombatEntity executor, CombatEntity[] targets)
    {
        combatRouter.onRoutingComplete += HandleAttackComplete;
        combatRouter.SaveRotation();
        combatRouter.BeginRouting(targets[currentTargetIndex++].gameObject);
        executor.TriggerRunningAnimation();
    }

    private void InitializeHandler(CombatEntity executor, CombatEntity[] targets)
    {
        this.executor = executor;
        this.targets = targets;
        this.initialPosition = executor.gameObject.transform.position;
        this.currentTargetIndex = 0;
        combatRouter = executor.gameObject.GetComponent<CombatRouter>();
    }

    protected virtual void HandleAttackComplete(object sender, EventArgs e)
    {
        CombatEntity targetCombatEntity = ((CombatRouter)sender).GetComponent<CombatEntity>();

        //targetCombatEntity.TriggerHitAnimation();
        executor.TriggerIdleAnimation();
        targetCombatEntity.GetComponent<Animator>().SetTrigger(combatActionConfig.combatActionType.ToString());

        targetCombatEntity.onAnimationComplete += HandleAttackAnimationComplete;
    }

    protected void ProcessNextTarget()
    {
        // Check for more targets, if any exist then move to those
        if (currentTargetIndex < targets.Length)
        {
            if (!combatActionConfig.requireRouting)
            {
                HandleAttackComplete(executor.GetComponent<CombatRouter>(), EventArgs.Empty);
                return;
            }

            combatRouter.BeginRouting(targets[currentTargetIndex].gameObject);
            executor.TriggerRunningAnimation();

            return;
        }

        // Otherwise, route back to original position
        if (!combatActionConfig.requireRouting)
        {
            HandleReturnToPositionComplete(executor, EventArgs.Empty);
            return;
        }

        combatRouter.onRoutingComplete -= HandleAttackComplete;
        combatRouter.onRoutingComplete += HandleReturnToPositionComplete;

        executor.TriggerRunningAnimation();
        combatRouter.BeginRouting(initialPosition);
    }

    protected void HandleAttackAnimationComplete()
    {
        ActionPerformedArgs targetActionResult = HandleActionExecution();
        CombatEventSystem.instance.OnActionPerformed(this, targetActionResult);

        ProcessNextTarget();
    }

    protected abstract ActionPerformedArgs HandleActionExecution();

    protected virtual void HandleReturnToPositionComplete(object sender, EventArgs e)
    {
        executor.onAnimationComplete -= HandleAttackAnimationComplete;
        combatRouter.onRoutingComplete -= HandleReturnToPositionComplete;

        combatRouter.RestoreRotation();
        executor.TriggerIdleAnimation();
        
        OnCombatActionComplete(this, new ActionPerformedArgs { TargetedUnits = targets });
    }


    public void ApplyEffects(List<CombatEffectConfig> effectsToApply, params CombatEntity[] targets)
    {
        foreach (CombatEntity targetEntity in targets)
        {
            List<IEffectHandler> effectHandlerInstances = new List<IEffectHandler>();

            foreach (CombatEffectConfig targetEffect in effectsToApply)
            {
                Type combatEffectType = Type.GetType(targetEffect.effectHandlerClassName);
                IEffectHandler effectHandlerInterfaceInstance = (IEffectHandler)Activator.CreateInstance(combatEffectType);

                effectHandlerInterfaceInstance.InitializeEffect(targetEffect, executor, targetEntity);
                effectHandlerInstances.Add(effectHandlerInterfaceInstance);
            }
            targetEntity.ApplyEffects(effectHandlerInstances);
        }
    }

    public void OnCombatActionComplete(object sender, ActionPerformedArgs args)
    {
        onCombatActionComplete?.Invoke(sender, args);
    }
}
