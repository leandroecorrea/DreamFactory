using System.Collections.Generic;
using System.Linq;

public abstract class BaseEnemyAI : IEnemyAI
{
    protected Dictionary<string, CombatActionConfig> actions;
    protected CombatContext context;
    protected AIChoice turnChoice = new AIChoice();
    protected abstract string[] ActionKeys { get; }
   

    public IContextSetting SetActions(List<CombatActionConfig> availableActions)
    {
        actions = availableActions.ToDictionary(x => x.actionName);
        return this;
    }

    public IAttackGenerator SetContext(CombatContext combatContext)
    {
        context = new CombatContext
        {
            currentTurnEntity = combatContext.currentTurnEntity,
            playerParty = combatContext.enemyParty,
            enemyParty = combatContext.playerParty,
        };
        return this;
    }

    public virtual AIChoice AndTarget()
    {
        var choiceTargets = turnChoice.action.targetStrategy.GetTargets(context);
        turnChoice.targets = new[] { GetRandomEntity(choiceTargets) };
        return turnChoice;
    }

    public virtual ITargetGenerator ThenGenerateAttack()
    {
        turnChoice.action = ChooseAnAction();
        return this;
    }

    protected abstract CombatActionConfig ChooseAnAction();

    protected  CombatActionConfig RandomAction()
    {
        var randomIndex = UnityEngine.Random.Range(0, actions.Count);
        return actions.Values.ToList()[randomIndex];
    }

    protected bool IsAbleToPerformAllAbilities()
    {
        for(int i = 0; i < ActionKeys.Length; i++)
        {
            if (!actions.ContainsKey(ActionKeys[i]))
                return false;
        }
        return true;
    }

    protected CombatEntity GetRandomEntity(List<CombatEntity> entities)
    {
        var randomIndex = UnityEngine.Random.Range(0, entities.Count - 1);
        return entities[randomIndex];
    }
}

