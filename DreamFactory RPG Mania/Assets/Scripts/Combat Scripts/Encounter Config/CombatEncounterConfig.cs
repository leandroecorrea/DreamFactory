using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Encounter", menuName = "Combat/New Combat Encounter")]
public class CombatEncounterConfig : ScriptableObject
{
    public EncounterHistory.Encounters encounter;
    public List<CombatEntityConfig> enemies;

    public string encounterScene;
    [Tooltip("The scene that is loaded once combat is complete, leave blank for the origin scene to be reloaded")] public string sceneToReturnTo;

    public string targetCombatSceneName;
    public int experienceReward;
}
