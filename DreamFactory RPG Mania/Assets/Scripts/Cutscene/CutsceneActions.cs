using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneActions : MonoBehaviour
{
    [Header("Combat Action Config")]
    [SerializeField] private CombatEncounterConfig afterCutsceneCombatEncounter;

    public void StartAfterCutsceneCombatEncounter()
    {
        Debug.Log("Hello There");
        CombatTransitionManager.instance.InitializeCombatTransition(afterCutsceneCombatEncounter);
    }
}
