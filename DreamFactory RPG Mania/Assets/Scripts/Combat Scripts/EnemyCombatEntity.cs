using System;
using System.Collections;
using UnityEngine;

public class EnemyCombatEntity : CombatEntity
{
    [Header("Test")]
    [SerializeField] private CombatActionConfig combatAction;
    [SerializeField] private BehaviourTreeClassName behaviourTreeClassName;
    private IEnemyAI behaviourTree;

    private void OnEnable()
    {
        var type = Type.GetType(behaviourTreeClassName.ToString());
        behaviourTree = (IEnemyAI)Activator.CreateInstance(type);
    }

    public override void StartTurn(CombatContext turnContext)
    {
        
        if (IsDisabled)
        {
            SkipTurn(turnContext);
            return;
        }        
        var turnChoice = behaviourTree.SetActions(entityConfig.actions)
                                      .SetContext(turnContext)
                                      .ThenGenerateAttack()
                                      .AndTarget();
        CombatUIManager.instance.UpdateInformation($"Enemy {entityConfig.Name} performed {turnChoice.action.actionName}!");
        PerformAction(turnChoice.action, turnChoice.targets);
    }
}
