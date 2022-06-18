using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Queue<CombatEntity> combatEntities;
    [HideInInspector] public CombatEntity currentTurnEntity;

    public void InitializeTurns(List<CombatEntity> entities)
    {
        combatEntities = new Queue<CombatEntity>(entities.OrderByDescending(x => x.entityConfig.baseSpeed));
        currentTurnEntity = combatEntities.Dequeue();

        StartCombat();
    }

    public void StartCombat()
    {
        // Start Current Combat Entity Turn
    }


    public void EndTurn()
    {
        combatEntities.Enqueue(currentTurnEntity);
        currentTurnEntity = combatEntities.Dequeue();
    }
}
