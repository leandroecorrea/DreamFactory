using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatEntity : CombatEntity
{
    [Header("Test")]
    [SerializeField] private CombatActionConfig combatAction;
    

    public override void StartTurn(CombatContext turnContext)
    {
        CombatEntity target = turnContext.playerParty[0];

        PerformAction(combatAction, target);
    }
}
