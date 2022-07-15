using System;
using System.Collections.Generic;
using System.Linq;

public class SheppaEnemyAI : BaseEnemyAI
{
    protected override string[] ActionKeys => new[] { "Amnesia", "REM", "Physical Attack", "Freeze", "Anxiety", "Lucid", "Coma" };
    private Func<List<CombatEntity>, CombatEntity[]> AimingChoice;

    protected override CombatActionConfig ChooseAnAction()
    {        
        var random = UnityEngine.Random.Range(0, 100);
        if (!IsAbleToPerformAllAbilities())
            return RandomAction();
        if(context.currentTurnEntity.CurrentHP < context.currentTurnEntity.entityConfig.baseHP / 2)
            return actions["REM"];                    
        if (random < 55)
            return actions["Physical Attack"];
        if (random < 65)
            return actions["Freeze"];
        if (random < 75)
            return actions["Amnesia"];
        if (random < 85)
            return actions["Anxiety"];
        if (random < 95)
            return actions["Lucid"];
        else
            return actions["Coma"];
    }

    public override ITargetGenerator ThenGenerateAttack()
    {
        base.ThenGenerateAttack();
        return this;
    }
    public override AIChoice AndTarget()
    {
        SetTargetChoice();
        var actionTargets = turnChoice.action.targetStrategy.GetTargets(context);
        turnChoice.targets = AimingChoice(actionTargets);
        return turnChoice;
    }

    private void SetTargetChoice()
    {   
        var baseDamage = turnChoice.action.baseEffectiveness;
        if (baseDamage > 0)
        {
            var orderedEntitiesByHP = OrderByHP(context.enemyParty);
            if (orderedEntitiesByHP[0].CurrentHP < baseDamage * 0.75 || orderedEntitiesByHP.Count < 2 || turnChoice.action.IsHeal)
            {
                AimingChoice = AimTheWeakest;
                return;
            }
            AimingChoice = AimTheSecondWeakest;
        }
        else
        {
            AimingChoice = AimTheOneWithLessEffects;
        }
    }


    public CombatEntity[] AimTheOneWithLessEffects(List<CombatEntity> combatEntities)
    {
        return new[] { combatEntities.OrderByDescending(x=> x.EffectsCaused.Count).FirstOrDefault() };
    }


    public CombatEntity[] AimTheWeakest(List<CombatEntity> combatEntities)
    {
        return new[] { OrderByHP(combatEntities).FirstOrDefault() };
    }

    public CombatEntity[] AimTheSecondWeakest(List<CombatEntity> combatEntities)
    {
        return new[] { OrderByHP(combatEntities)[1] };
    }

    private IList<CombatEntity> OrderByHP(List<CombatEntity> combatEntities)
    {
        return combatEntities.OrderBy(x => x.CurrentHP).ToList();
    }
}


