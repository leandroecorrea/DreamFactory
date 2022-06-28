using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatEntity : CombatEntity
{
    [Header("Test")]
    [SerializeField] private CombatActionConfig combatAction;
    

    public override void StartTurn(CombatContext turnContext)
    {
        //this.entityConfig.actions;
        //AI.GetActionFor(turnContext)
        //base.StartTurn(turnContext);
        if(IsDisabled)
        {
            SkipTurn(turnContext);
            return;
        }
        CombatEntity target = turnContext.playerParty[0];
        PerformAction(combatAction, target);
    }

}


public class BaseEnemyBehaviourTree
{
    private static CombatContext _context;
    private static List<ICombatAction> _availableActions;

    private BaseEnemyBehaviourTree()
    {

    }
    public static BaseEnemyBehaviourTree RequireActionFor(CombatContext combatContext)
    {
         _context = combatContext;
        return new BaseEnemyBehaviourTree();
    }
    public CombatActionConfig From(List<ICombatAction> availableActions)
    {
        //Apply logic to decide which behaviour should be executed
        _availableActions = availableActions;
        return _availableActions[0].combatActionConfig;
    }
}
