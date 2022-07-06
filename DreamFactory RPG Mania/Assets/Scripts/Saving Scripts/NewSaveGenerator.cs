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
                { SaveKeys.CURRENT_STORY_POINT, StoryPointKeys.StoryKeys.PT1_Boss_Visit },
                { SaveKeys.UNLOCKED_PARTY_MEMBERS, new List<string> { "PUFFER" } }
            };
        }
    }
}
