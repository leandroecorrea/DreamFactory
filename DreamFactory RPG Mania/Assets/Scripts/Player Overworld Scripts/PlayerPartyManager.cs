using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPartyManager
{
    private static List<PlayerPartyMemberConfig> _unlockedPartyMembers;
    private static HashSet<string> _unlockedPartyMemberIds;

    public static List<PlayerPartyMemberConfig> UnlockedPartyMembers
    {
        get
        {
            if (_unlockedPartyMembers == null)
            {
                RetrievePartyMembersFromSave();
            }

            return _unlockedPartyMembers;
        }
    }

    private static void RetrievePartyMembersFromSave()
    {
        PlayerPartyMemberConfig[] allPartyMemberConfigs = Resources.LoadAll<PlayerPartyMemberConfig>("Party Member Configs");

        _unlockedPartyMemberIds = PlayerProgression.GetPlayerData<HashSet<string>>(SaveKeys.UNLOCKED_PARTY_MEMBERS);
        _unlockedPartyMembers = new List<PlayerPartyMemberConfig>();

        foreach (PlayerPartyMemberConfig partyMemberConfig in allPartyMemberConfigs)
        {
            if (_unlockedPartyMemberIds.Contains(partyMemberConfig.partyMemberId))
            {
                _unlockedPartyMembers.Add(partyMemberConfig);
            }
        }
    }

    public static void UnlockPartyMemberById(PlayerPartyMemberConfig newPartyMemberConfig)
    {
        if (_unlockedPartyMemberIds.Contains(newPartyMemberConfig.partyMemberId))
        {
            Debug.LogError($"Trying to add ${newPartyMemberConfig.name} to party when they're already added");
            return;
        }

        _unlockedPartyMemberIds.Add(newPartyMemberConfig.partyMemberId);
        _unlockedPartyMembers.Add(newPartyMemberConfig);
    }

    public static List<CombatEntityConfig> GetAllUnlockedCombatConfigs()
    {
        List<CombatEntityConfig> combatEntityConfigs = new List<CombatEntityConfig>();

        foreach(PlayerPartyMemberConfig partyMemberConfig in UnlockedPartyMembers)
        {
            combatEntityConfigs.Add(partyMemberConfig.combatEntityConfig);
        }

        return combatEntityConfigs;
    }
}
