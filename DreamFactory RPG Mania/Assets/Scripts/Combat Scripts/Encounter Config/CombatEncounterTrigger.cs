using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatEncounterTrigger : MonoBehaviour
{
    [Header("Combat Encounter Config")]
    [SerializeField] private CombatEncounterConfig targetEncounter;

    // This will need to be replaced by some sort of Player Party Manager
    // Using for testing ATM
    [SerializeField] private CombatEntityConfig playerEntityConfig;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //CombatManager.currentStartRequest = new CombatStartRequest(targetEncounter.enemies, new List<CombatEntityConfig> { playerEntityConfig }, SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene(targetEncounter.targetCombatSceneName);

            CombatTransitionManager.instance.InitializeCombatTransition(
                new CombatStartRequest(targetEncounter.enemies, new List<CombatEntityConfig> { playerEntityConfig }, SceneManager.GetActiveScene().name),
                targetEncounter.targetCombatSceneName
            );
        }
    }
}
