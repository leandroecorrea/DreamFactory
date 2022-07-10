using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketDimensionManager : MonoBehaviour
{
    [Header("Completion Settings")]
    [SerializeField] private StoryPointKeys.StoryKeys afterCompletionStoryPoint;
    [SerializeField] private string afterCompletionReloadScene;
    [SerializeField] private Vector3 afterCompletionSpawningPoint;

    private void OnEnable()
    {
        CombatEncounterTrigger[] allEncounterTriggers = GameObject.FindObjectsOfType<CombatEncounterTrigger>();

        // This assumes all the triggers were completed and destroyed by the EncounterManager
        // Mark this as complete
        if (allEncounterTriggers == null || allEncounterTriggers.Length == 0)
        {
            MarkPocketDimensionAsComplete();
            return;
        }

        // Encounter Triggers still exist, make sure they haven't been completed
        // If all remaining triggers are completed, run Completion Logic
        List<EncounterHistory.Encounters> completedEncounters = EncounterHistory.EncountersFinished;
        bool areRemainingEncountersCompleted = true;

        foreach(CombatEncounterTrigger encounterTrigger in allEncounterTriggers)
        {
            if (!completedEncounters.Contains(encounterTrigger.targetEncounter.encounter))
            {
                areRemainingEncountersCompleted = false;
                break;
            }
        }

        if (areRemainingEncountersCompleted)
        {
            MarkPocketDimensionAsComplete();
        }
    }

    public void MarkPocketDimensionAsComplete()
    {
        StoryManager.UpdateCurrentStoryKey(afterCompletionStoryPoint);

        SceneTransitionManager.LoadNewSingleScene(afterCompletionReloadScene);
        PlayerOverworldPersistance.persistance.transform.position = afterCompletionSpawningPoint;
    }
}
