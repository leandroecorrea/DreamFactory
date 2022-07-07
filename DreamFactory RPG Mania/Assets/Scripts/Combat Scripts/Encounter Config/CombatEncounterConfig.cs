using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Encounter", menuName = "Combat/New Combat Encounter")]
public class CombatEncounterConfig : ScriptableObject
{
    public EncounterHistory.Encounters encounter;
    public List<CombatEntityConfig> enemies;
    public string encounterScene;
    public string targetCombatSceneName;
    public int experienceReward;
}
