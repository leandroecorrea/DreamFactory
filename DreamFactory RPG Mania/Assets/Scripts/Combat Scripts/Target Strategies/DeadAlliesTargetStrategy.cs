using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dead Allies Target Strategy Config", menuName = "Combat/Target Strategies/Dead allies Targets Config")]
public class DeadAlliesTargetStrategy : TargetStrategy
{
    public override List<CombatEntity> GetTargets(CombatContext context)
        => context.playerParty.Where(x => x.CurrentHP <= 0).ToList();
}
