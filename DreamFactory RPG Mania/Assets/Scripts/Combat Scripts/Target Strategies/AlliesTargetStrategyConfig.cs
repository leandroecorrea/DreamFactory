using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Allies Target Strategy Config", menuName = "Combat/Target Strategies/New Allies Target Config")]

public class AlliesTargetStrategyConfig : TargetStrategy
{
    public override List<CombatEntity> GetTargets(CombatContext context)
        => context.playerParty;
}
