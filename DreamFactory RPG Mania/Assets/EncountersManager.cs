using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncountersManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawningPoints;
    private string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    void OnEnable()
    {
        GetEncountersForScene();               
    }

    private void GetEncountersForScene()
    {
        var res = Resources.LoadAll<CombatEncounterConfig>("Combat Encounters");
        var encountersSaved = EncounterHistory.EncountersFinished;
        var encounters = res.Where(x=> x.encounterScene == sceneName && !encountersSaved.Contains(x.encounter)).ToArray();        
        for (int i = 0; i < encounters.Length; i++)
        {
            var enemyPrefab = encounters[i].enemies[0].combatEntityPrefab;
            var prefab = Instantiate(enemyPrefab, spawningPoints[i].transform.position, Quaternion.identity);
            var component = prefab.AddComponent<CombatEncounterTrigger>();
            component.targetEncounter = encounters[i];
            var trigger = prefab.AddComponent<SphereCollider>();
            trigger.radius = 3;
            trigger.isTrigger = true;
        }        
    }
}