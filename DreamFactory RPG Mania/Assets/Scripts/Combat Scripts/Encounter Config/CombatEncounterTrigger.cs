using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatEncounterTrigger : MonoBehaviour
{
    [Header("Combat Encounter Config")]
    [SerializeField] private CombatEncounterConfig targetEncounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<SphereCollider>().enabled = false;
            CombatTransitionManager.instance.InitializeCombatTransition(
                new CombatStartRequest(targetEncounter.enemies, PlayerPartyManager.GetAllUnlockedCombatConfigs(), SceneManager.GetActiveScene().name, targetEncounter.experienceReward),
                targetEncounter.targetCombatSceneName
            );
        }
    }
}
