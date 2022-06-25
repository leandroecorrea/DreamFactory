using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New All Target Strategy Config", menuName = "Combat/Target Strategies/New All Targets Config")]
public class AllTargetStrategyConfig : TargetStrategy
{
    public override List<CombatEntity> GetTargets(CombatContext context)
        => context.enemyParty.Concat(context.playerParty).ToList();
}