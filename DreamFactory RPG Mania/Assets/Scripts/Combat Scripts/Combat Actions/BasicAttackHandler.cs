using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackHandler : ICombatAction
{
    public event EventHandler<ActionPerformedArgs> onCombatActionComplete;

    private CombatRouter combatRouter;
    private CombatEntity executor;
    private CombatEntity[] targets;
    private Vector3 initialPosition;
    private int currentTargetIndex;
    private int fixedDamage = 1000;

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
        combatRouter.BeginRouting(targets[0].transform.position);
    }

    private void HandleMoveToAttackTargetComplete(object sender, EventArgs e)
    {
        // Damage the current target
        targets[currentTargetIndex].TakeDamage(fixedDamage);
        Debug.Log($"Target {targets[currentTargetIndex].gameObject.name} took {fixedDamage} damage, its current HP is {targets[currentTargetIndex].CurrentHP}");

        currentTargetIndex++;

        // Check for more targets, if any exist then move to those
        if (currentTargetIndex < targets.Length)
        {
            combatRouter.BeginRouting(targets[currentTargetIndex].transform.position);
            return;
        }

        // Otherwise, route back to original position
        combatRouter.onRoutingComplete -= HandleMoveToAttackTargetComplete;
        combatRouter.onRoutingComplete += HandleReturnToPositionComplete;

        combatRouter.BeginRouting(initialPosition);
        //TODO uncomment when merging CombatEventSystem
        CombatEventSystem.instance.OnActionPerformed(this, new ActionPerformedArgs { TargetedUnits = targets, ActionPerformed = this });
    }
    private void HandleReturnToPositionComplete(object sender, EventArgs e)
    {
        combatRouter.onRoutingComplete -= HandleReturnToPositionComplete;
        onCombatActionComplete?.Invoke(this, new ActionPerformedArgs { TargetedUnits = targets });
    }
}