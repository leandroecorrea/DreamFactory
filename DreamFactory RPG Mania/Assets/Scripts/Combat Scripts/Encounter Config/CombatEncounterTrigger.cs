using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatEncounterTrigger : MonoBehaviour
{
    [Header("Combat Encounter Config")]
    [SerializeField] public CombatEncounterConfig targetEncounter;

    private void OnTriggerEnter(Collider other)
    {
        // Safeguard incase completed combat encounter remains active
        List<EncounterHistory.Encounters> completedEncounters = EncounterHistory.EncountersFinished;
        if (completedEncounters.Contains(targetEncounter.encounter))
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            GetComponent<SphereCollider>().enabled = false;

            CombatStartRequest startRequest = new CombatStartRequest(
                targetEncounter.enemies,
                PlayerPartyManager.GetAllUnlockedCombatConfigs(),
                SceneManager.GetActiveScene().name,
                targetEncounter.experienceReward,
                targetEncounter.encounter
            );

            if (targetEncounter.sceneToReturnTo != "")
            {
                startRequest.OverrideReturnScene(targetEncounter.sceneToReturnTo);
            }

            CombatTransitionManager.instance.InitializeCombatTransition(
                startRequest,
                targetEncounter.targetCombatSceneName
            );           
        }
    }
}
