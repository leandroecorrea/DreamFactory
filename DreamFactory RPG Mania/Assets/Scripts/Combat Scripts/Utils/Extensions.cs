using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static CombatEntityConfig GetCombatEntityConfig(this CombatEntityConfig[] combatEntityConfigs, string id)
    {
        for (int i = 0; i < combatEntityConfigs.Length; i++)
        {
            if (combatEntityConfigs[i].Id == id)
                return combatEntityConfigs[i];
        }
        return null;
    }
}
