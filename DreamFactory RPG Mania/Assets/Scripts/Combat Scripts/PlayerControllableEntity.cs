using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllableEntity : CombatEntity
{
    public void SkipTurn(CombatContext turnContext)
    {
        Debug.Log("Player is disabled");
        EndTurn(turnContext);
        return;
    }
}
