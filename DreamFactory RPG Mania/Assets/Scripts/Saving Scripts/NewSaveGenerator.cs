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
                { SaveKeys.CURRENT_STORY_POINT, StoryPointKeys.StoryKeys.PT2_Dream_Machine_2_Pre_Fix },
                { SaveKeys.UNLOCKED_PARTY_MEMBERS, new List<string> { "PUFFER", "THEO" } },
                { SaveKeys.COMBAT_ENCOUNTERS_COMPLETED, new List<EncounterHistory.Encounters>{ } }
            };
        }
    }
}
