using System;

using System.Linq;
using UnityEngine;

public class BaseActionExecution : ActionExecution
{
    public override void Execute(CombatActionRequest request)
    {
        var action = request.ActionChosen;
        var entity = request.CurrentEntity;
        var target = request.Targets;
        Type combatActionType = Type.GetType(action.actionHandlerClassName);
        ICombatAction attackHandlerInterface = (ICombatAction)Activator.CreateInstance(combatActionType);
        attackHandlerInterface.combatActionConfig = action;
        attackHandlerInterface.onCombatActionComplete += entity.HandleCombatActionComplete;
        attackHandlerInterface.ExecuteAction(entity, target);
        entity.LastExecutedAction = entity.entityConfig.actions.Where(x => action).FirstOrDefault();
    }
}
