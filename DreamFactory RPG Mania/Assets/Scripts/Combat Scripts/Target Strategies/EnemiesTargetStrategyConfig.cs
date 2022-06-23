using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemies Target Strategy Config", menuName = "Combat/Target Strategies/New Enemies Target Config")]
public class EnemiesTargetStrategyConfig : TargetStrategy
{
    public override List<CombatEntity> GetTargets(CombatContext context)
        => context.enemyParty;
}
