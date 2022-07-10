using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncountersManager : MonoBehaviour
{
    void OnEnable()
    {
        GetEncountersForScene();               
    }

    private void GetEncountersForScene()
    {
        CombatEncounterTrigger[] allCombatTriggers = GameObject.FindObjectsOfType<CombatEncounterTrigger>();

        if (allCombatTriggers != null && allCombatTriggers.Length > 0)        
        {
            // Disable All Completed Encounters
            List<EncounterHistory.Encounters> completedEncounters = EncounterHistory.EncountersFinished;

            foreach(CombatEncounterTrigger encounterTrigger in allCombatTriggers)
            {
                if (completedEncounters.Contains(encounterTrigger.targetEncounter.encounter))
                {
                    GameObject.Destroy(encounterTrigger.gameObject);
                }
            }
        }           
    }
}