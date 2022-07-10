using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncountersManager : MonoBehaviour
{
    // [SerializeField] private GameObject[] spawningPoints;
    // private string sceneName;

    // private void Awake()
    // {
        // sceneName = SceneManager.GetActiveScene().name;
    // }

    void OnEnable()
    {
        GetEncountersForScene();               
    }

    private void GetEncountersForScene()
    {
        CombatEncounterTrigger[] allCombatTriggers = GameObject.FindObjectsOfType<CombatEncounterTrigger>();

        if (allCombatTriggers != null && allCombatTriggers.Length > 0)
        for (int i = 0; i < encounters.Length; i++)
        for (int i = 0; i < encounters.Length; i++)
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

        //var res = Resources.LoadAll<CombatEncounterConfig>("Combat Encounters");
        //var encountersSaved = EncounterHistory.EncountersFinished;
        //var encounters = res.Where(x=> x.encounterScene == sceneName && !encountersSaved.Contains(x.encounter)).ToArray();

        //List<EncounterHistory.Encounters> completedEncounters = PlayerProgression.GetPlayerData<List<EncounterHistory.Encounters>>(SaveKeys.COMBAT_ENCOUNTERS_COMPLETED);

        ////ask for save for unavailable encounters
        //for (int i = 0; i < encounters.Length; i++)
        //{
        //    if (completedEncounters.Contains(encounters[i].encounter)) { continue; }

        //    var enemyPrefab = encounters[i].enemies[0].combatEntityPrefab;
        //    var prefab = Instantiate(enemyPrefab, spawningPoints[i].transform.position, Quaternion.identity);

        //    var component = prefab.AddComponent<CombatEncounterTrigger>();
        //    component.targetEncounter = encounters[i];

        //    var trigger = prefab.AddComponent<SphereCollider>();
        //    trigger.radius = 3;
        //    trigger.isTrigger = true;
        //}        
    }
}