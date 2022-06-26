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
                { SaveKeys.UNLOCKED_PARTY_MEMBERS, new HashSet<string> { "PUFFER", "THEO" } }
            };
        }
    }
}
