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
                { SaveKeys.CURRENT_STORY_POINT, StoryPointKeys.StoryKeys.INT_Sal_Controls_Intro },
                { SaveKeys.UNLOCKED_PARTY_MEMBERS, new List<string> { "PUFFER" } }
            };
        }
    }
}
