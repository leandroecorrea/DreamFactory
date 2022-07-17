using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NewSaveGenerator
{
    public static Dictionary<string, object> newSaveDefaultInfo
    {
        get
        {
            return new Dictionary<string, object>
            {
                { SaveKeys.CURRENT_STORY_POINT, StoryPointKeys.StoryKeys.CLI_Shepherd_Intro },
                { SaveKeys.UNLOCKED_PARTY_MEMBERS, new List<string> { "PUFFER", "THEO", "MINDY" } },
                { SaveKeys.COMBAT_ENCOUNTERS_COMPLETED, new List<EncounterHistory.Encounters>{ } }
            };
        }
    }
}
