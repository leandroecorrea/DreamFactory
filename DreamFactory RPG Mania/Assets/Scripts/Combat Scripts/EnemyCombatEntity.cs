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
        PerformAction(turnChoice.action, turnChoice.targets);
    }
}
